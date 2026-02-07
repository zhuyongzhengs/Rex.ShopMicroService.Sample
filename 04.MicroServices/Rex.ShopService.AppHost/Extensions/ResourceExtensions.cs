namespace Rex.ShopService.AppHost.Extensions
{
    /// <summary>
    /// 资源注入扩展
    /// </summary>
    public static class ResourceExtensions
    {
        /// <summary>
        /// 中间件服务配置
        /// </summary>
        /// <remarks>
        /// 基础数据库、Redis、MongoDB、RabbitMQ、MinIO
        /// </remarks>
        public static IResourceBuilder<ProjectResource> WithCommonReferences(this IResourceBuilder<ProjectResource> projectBuilder,
            IResourceBuilder<RedisResource> redis,
            IResourceBuilder<PostgresDatabaseResource> sharedDb,
            IResourceBuilder<MongoDBDatabaseResource> auditLog,
            IResourceBuilder<RabbitMQServerResource> rabbitmq,
            IResourceBuilder<MinioContainerResource>? minio = null)
        {
            projectBuilder
                .WithReference(sharedDb, connectionName: "Default")
                .WaitFor(sharedDb)
                .WithReference(redis, connectionName: "Redis")
                .WaitFor(redis)
                .WithReference(auditLog, connectionName: "AbpAuditLogging")
                .WaitFor(auditLog)

                //  CAP / CapRabbitMQ 映射
                .WithReference(rabbitmq)
                .WaitFor(rabbitmq)
                .WithEnvironment("CapRabbitMQ__HostName", rabbitmq.Resource.PrimaryEndpoint.Property(EndpointProperty.Host))
                .WithEnvironment("CapRabbitMQ__Port", rabbitmq.Resource.PrimaryEndpoint.Property(EndpointProperty.Port))
                .WithEnvironment("CapRabbitMQ__UserName", rabbitmq.Resource.UserNameParameter)
                .WithEnvironment("CapRabbitMQ__Password", rabbitmq.Resource.PasswordParameter)
                .WithEnvironment("CapRabbitMQ__GroupName", projectBuilder.Resource.Name);

            if (minio != null)
            {
                // MinIO / BlobStoringProvider 映射
                projectBuilder
                    .WithReference(minio)
                    .WaitFor(minio)
                    .WithEnvironment("BlobStoringProvider__Endpoint", $"{minio.Resource.PrimaryEndpoint.Property(EndpointProperty.Host)}:{minio.Resource.PrimaryEndpoint.Property(EndpointProperty.Port)}")
                    .WithEnvironment("BlobStoringProvider__AccessKey", minio.Resource.RootUser)
                    .WithEnvironment("BlobStoringProvider__SecretKey", minio.Resource.PasswordParameter)
                    .WithEnvironment("BlobStoringProvider__Ssl", "false")
                    .WithEnvironment("BlobStoringProvider__Provider", "Minio");
            }
            return projectBuilder;
        }

        /// <summary>
        /// 迁移项目配置
        /// </summary>
        public static IResourceBuilder<ProjectResource> AddMigration<TProject>(
            this IDistributedApplicationBuilder builder,
            string name,
            IResourceBuilder<ProjectResource> apiService,
            IResourceBuilder<RedisResource> redis,
            IResourceBuilder<PostgresDatabaseResource> db,
            string connectionName = "Default") where TProject : IProjectMetadata, new()
        {
            var migrationProject = builder.AddProject<TProject>(name)
                .WithReference(db, connectionName)
                .WaitFor(db)
                .WithReference(redis, connectionName: "Redis")
                .WaitFor(redis);
            // 让 API 服务等待迁移项目执行完成后再启动
            apiService.WaitForCompletion(migrationProject);
            return migrationProject;
        }

        /// <summary>
        /// 聚合服务配置
        /// </summary>
        public static IResourceBuilder<ProjectResource> WithAggregationReferences(this IResourceBuilder<ProjectResource> projectBuilder,
            IResourceBuilder<RedisResource> redis,
            IResourceBuilder<PostgresDatabaseResource> sharedDb,
            IResourceBuilder<RabbitMQServerResource> rabbitmq)
        {
            return projectBuilder
                .WithReference(sharedDb, connectionName: "Default")
                .WaitFor(sharedDb)
                .WithReference(redis, connectionName: "Redis")
                .WaitFor(redis)

                //  CAP / CapRabbitMQ 映射
                .WithReference(rabbitmq)
                .WaitFor(rabbitmq)
                .WithEnvironment("CapRabbitMQ__HostName", rabbitmq.Resource.PrimaryEndpoint.Property(EndpointProperty.Host))
                .WithEnvironment("CapRabbitMQ__Port", rabbitmq.Resource.PrimaryEndpoint.Property(EndpointProperty.Port))
                .WithEnvironment("CapRabbitMQ__UserName", rabbitmq.Resource.UserNameParameter)
                .WithEnvironment("CapRabbitMQ__Password", rabbitmq.Resource.PasswordParameter)
                .WithEnvironment("CapRabbitMQ__GroupName", projectBuilder.Resource.Name);
        }
    }
}