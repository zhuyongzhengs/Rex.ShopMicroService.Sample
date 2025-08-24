<template>
  <el-form
    ref="loginFormRef"
    :model="loginForm"
    :rules="loginRules"
    size="large"
    class="login-content-form"
  >
    <el-form-item class="login-animation2" prop="username">
      <el-input
        text
        :placeholder="$t('message.account.accountPlaceholder1')"
        v-model="loginForm.username"
        autofocus
        clearable
        autocomplete="off"
      >
        <template #prefix>
          <el-icon class="el-input__icon"><ele-User /></el-icon>
        </template>
      </el-input>
    </el-form-item>
    <el-form-item class="login-animation3" prop="password">
      <el-input
        :type="loginState.isShowPassword ? 'text' : 'password'"
        :placeholder="$t('message.account.accountPlaceholder2')"
        v-model="loginForm.password"
        autocomplete="off"
        @keyup.enter="loginValidate(loginFormRef)"
      >
        <template #prefix>
          <el-icon class="el-input__icon"><ele-Unlock /></el-icon>
        </template>
        <template #suffix>
          <i
            class="iconfont el-input__icon login-content-password"
            :class="loginState.isShowPassword ? 'icon-yincangmima' : 'icon-xianshimima'"
            @click="loginState.isShowPassword = !loginState.isShowPassword"
          >
          </i>
        </template>
      </el-input>
    </el-form-item>
    <el-form-item class="login-animation3" prop="verificationCode">
      <el-col :span="15">
        <el-input
          text
          maxlength="4"
          :placeholder="$t('message.account.accountPlaceholder3')"
          v-model="loginForm.verificationCode"
          clearable
          autocomplete="off"
          @keyup.enter="loginValidate(loginFormRef)"
        >
          <template #prefix>
            <el-icon class="el-input__icon"><ele-Position /></el-icon>
          </template>
        </el-input>
      </el-col>
      <el-col :span="1"></el-col>
      <el-col :span="8" class="login-content-code">
        <el-image :src="loginState.captchaUrl" @click="loadCaptcha" title="点击刷新" />
      </el-col>
    </el-form-item>
    <el-form-item class="login-animation4">
      <el-button
        type="primary"
        class="login-content-submit"
        round
        v-waves
        @click="loginValidate(loginFormRef)"
        :loading="loginState.loading.signIn"
      >
        <span>{{ $t("message.account.accountBtnText") }}</span>
      </el-button>
    </el-form-item>
    <div class="font12 mt30 login-animation4 login-msg">
      {{ $t("message.mobile.msgText") }}
    </div>
  </el-form>
</template>

<script setup lang="ts" name="loginAccount">
import { ref, reactive, computed, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import type { FormInstance, FormRules } from "element-plus";
import { ElMessage, ElMessageBox } from "element-plus";
import { useI18n } from "vue-i18n";
import { initBackEndControlRoutes } from "/@/router/backEnd";
import { Session } from "/@/utils/storage";
import { getCode } from "/@/utils/other";
import { formatAxis } from "/@/utils/formatTime";
import { NextLoading } from "/@/utils/loading";
import { useLoginApi } from "/@/api/login/index";

// 定义变量内容
const { t } = useI18n();
const route = useRoute();
const router = useRouter();

// 登录状态
const loginState = reactive({
  isShowPassword: false,
  loading: {
    signIn: false,
  },
  captchaUrl: "",
});

// 登录表单
interface LoginForm {
  client_id: string;
  grant_type: string;
  username: string;
  password: string;
  scope: string;
  verificationKey: string;
  verificationCode: string;
}

const loginForm = reactive<LoginForm>({
  client_id: import.meta.env.VITE_AUTH_CLIENT_ID,
  grant_type: "password",
  username: "admin",
  password: "1q2w3E*",
  verificationKey: "",
  verificationCode: "",
  scope: import.meta.env.VITE_AUTH_SCOPE,
});

// 登录校验规则
const loginFormRef = ref<FormInstance>();
const loginRules = reactive<FormRules>({
  username: [{ required: true, message: "请输入用户名！", trigger: "blur" }],
  password: [{ required: true, message: "请输入密码！", trigger: "blur" }],
  verificationCode: [{ required: true, message: "请输入验证码！", trigger: "blur" }],
});

// 加载图片验证码
const loadCaptcha = () => {
  loginForm.verificationKey = getCode();
  loginState.captchaUrl = `${import.meta.env.VITE_API_URL}api/account/captcha?id=${
    loginForm.verificationKey
  }`;
  loginForm.verificationCode = "";
};

// 登录验证
const loginValidate = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  await formEl.validate((valid, fields) => {
    if (valid) {
      onSignIn();
    } else {
      console.warn("未验证通过!", fields);
    }
  });
};

// 登录
const onSignIn = async () => {
  loginState.loading.signIn = true;
  useLoginApi()
    .signIn(loginForm)
    .then(async (result) => {
      // 存储 token 到浏览器缓存
      Session.set("token", result.access_token); // 请求token

      // 添加完动态路由，再进行 router 跳转，否则可能报错 No match found for location with path "/"
      const isNoPower = await initBackEndControlRoutes();

      // 执行完 initBackEndControlRoutes，再执行 signInSuccess
      signInSuccess(isNoPower);
    })
    .catch((err) => {
      loadCaptcha();
      console.error("登录异常：", err);
      loginState.loading.signIn = false;
    });
};

// 登录成功后的跳转
const signInSuccess = (isNoPower: boolean | undefined) => {
  if (isNoPower) {
    ElMessageBox.alert("抱歉，该账号没有访问的菜单权限！");
    Session.clear();
  } else {
    // 初始化登录成功时间问候语
    let currentTimeInfo = currentTime.value;
    // 登录成功，跳到转首页
    // 如果是复制粘贴的路径，非首页/登录页，那么登录成功后重定向到对应的路径中
    if (route.query?.redirect) {
      router.push({
        path: <string>route.query?.redirect,
        query:
          Object.keys(<string>route.query?.params).length > 0
            ? JSON.parse(<string>route.query?.params)
            : "",
      });
    } else {
      router.push("/");
    }
    // 登录成功提示
    const signInText = t("message.signInText");
    ElMessage.success(`${currentTimeInfo}，${signInText}`);
    // 添加 loading，防止第一次进入界面时出现短暂空白
    NextLoading.start();
  }
  loginState.loading.signIn = false;
};

// 时间获取
const currentTime = computed(() => {
  return formatAxis(new Date());
});

// 页面加载完时
onMounted(() => {
  loadCaptcha();
});
</script>

<style scoped lang="scss">
.login-content-form {
  margin-top: 20px;
  @for $i from 1 through 4 {
    .login-animation#{$i} {
      opacity: 0;
      animation-name: error-num;
      animation-duration: 0.5s;
      animation-fill-mode: forwards;
      animation-delay: calc($i/10) + s;
    }
  }

  .login-content-password {
    display: inline-block;
    width: 20px;
    cursor: pointer;
    &:hover {
      color: #909399;
    }
  }
  .login-content-code {
    width: 100%;
    height: 40px;
    line-height: 40px;
    font-weight: bold;
    cursor: pointer;
    border: 1px #dcdfe6 solid;
    border-radius: 3px;
  }
  .login-content-submit {
    width: 100%;
    letter-spacing: 2px;
    font-weight: 300;
    margin-top: 15px;
  }

  .login-msg {
    color: var(--el-text-color-placeholder);
  }
}
</style>
