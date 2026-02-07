namespace Rex.ShopService.AppHost.Configurations
{
    /// <summary>
    /// 应用参数管理配置
    /// </summary>
    public class AppParameters
    {
        /// <summary>
        /// PostgreSQL数据库配置参数
        /// </summary>
        public PostgreSqlParameters PostgreSql { get; init; } = null!;

        /// <summary>
        /// RabbitMQ配置参数
        /// </summary>
        public RabbitMqParameters RabbitMq { get; init; } = null!;

        /// <summary>
        /// Redis配置参数
        /// </summary>
        public RedisParameters Redis { get; init; } = null!;

        /// <summary>
        /// Minio对象存储配置参数
        /// </summary>
        public MinioParameters Minio { get; init; } = null!;

        /// <summary>
        /// 从应用构建器创建参数实例
        /// </summary>
        public static AppParameters Create(IDistributedApplicationBuilder builder)
        {
            return new AppParameters
            {
                RabbitMq = RabbitMqParameters.Create(builder),
                PostgreSql = PostgreSqlParameters.Create(builder),
                Minio = MinioParameters.Create(builder),
                Redis = RedisParameters.Create(builder)
            };
        }
    }

    /// <summary>
    /// 数据库配置参数
    /// </summary>
    public class MinioParameters
    {
        public IResourceBuilder<ParameterResource> AccessKey { get; init; } = null!;
        public IResourceBuilder<ParameterResource> SecretKey { get; init; } = null!;

        public static MinioParameters Create(IDistributedApplicationBuilder builder)
        {
            return new MinioParameters
            {
                AccessKey = builder.AddParameter("minio-accesskey", "minioadmin"),
                SecretKey = builder.AddParameter("minio-secretkey", "minioadmin", secret: true)
            };
        }
    }

    /// <summary>
    /// PostgreSql数据库配置参数
    /// </summary>
    public class PostgreSqlParameters
    {
        public IResourceBuilder<ParameterResource> Username { get; init; } = null!;
        public IResourceBuilder<ParameterResource> Password { get; init; } = null!;

        public static PostgreSqlParameters Create(IDistributedApplicationBuilder builder)
        {
            return new PostgreSqlParameters
            {
                Username = builder.AddParameter("postgres-username", "postgres"),
                Password = builder.AddParameter("postgres-password", "abc12345", secret: true)
            };
        }
    }

    /// <summary>
    /// RabbitMQ配置参数
    /// </summary>
    public class RedisParameters
    {
        public IResourceBuilder<ParameterResource> Password { get; init; } = null!;

        public static RedisParameters Create(IDistributedApplicationBuilder builder)
        {
            return new RedisParameters
            {
                Password = builder.AddParameter("password", "abc12345", secret: true)
            };
        }
    }

    /// <summary>
    /// RabbitMQ配置参数
    /// </summary>
    public class RabbitMqParameters
    {
        public IResourceBuilder<ParameterResource> Username { get; init; } = null!;
        public IResourceBuilder<ParameterResource> Password { get; init; } = null!;

        public static RabbitMqParameters Create(IDistributedApplicationBuilder builder)
        {
            return new RabbitMqParameters
            {
                Username = builder.AddParameter("rabbitmq-username", "rex_shop"),
                Password = builder.AddParameter("rabbitmq-password", "abc12345", secret: true)
            };
        }
    }
}