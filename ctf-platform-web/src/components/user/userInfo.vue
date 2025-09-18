<template>
  <div class="card-box">
    <div>
      <SingleImageUpload v-model:value="userInfo.userImage" @success="handleAvatarSuccess" />
    </div>

    <div class="center-box">
      <div class="text-container">
        <span class="text-content">{{ userInfo.nickName }}</span>
        <button class="edit-icon" @click="handleEdit">
          <svg width="15" height="15" viewBox="0 -3 24 24" fill="none">
            <path
              d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.172l8.586-8.586z"
              stroke="currentColor" stroke-width="1" stroke-linecap="round" stroke-linejoin="round" />
          </svg>
        </button>
      </div>
    </div>

    <div class="center-box">
      <!-- 根据 userType 显示不同的标签 -->
      <el-tag v-if="userInfo.userType === 0" type="danger">{{ $t('user.admin') }}</el-tag>
      <el-tag v-else-if="userInfo.userType === 1" type="info">{{ $t('user.normalUser') }}</el-tag>
      <el-tag v-else-if="userInfo.userType === 2" type="success">{{ $t('user.betaUser') }}</el-tag>
    </div>
  </div>
</template>

<script setup lang="ts">
//官方引入
import { onMounted, reactive } from 'vue'

//插件引入

//自定义引入
import SingleImageUpload from '@/components/base/singleImageUpload.vue'
import type { text_req } from '@/api-services/models/base/text_req'
import apiClient from '@/api-services/apis'
import { ElMessage, ElMessageBox } from 'element-plus'
//资源引入

//数据
//用户信息
const userInfo = reactive({
  nickName: 'Unkonw User',
  userImage: 'https://img2.baidu.com/it/u=3233382939,3485050990&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=500',
  userType: 1 //枚举值 0 管理员，1 普通用户，2 内测用户
})

//方法
// 头像上传成功处理函数
const handleAvatarSuccess = (url: string) => {
  userInfo.userImage = url;
  saveNewImage();
}

/**
 * 修改昵称
 */
const handleEdit = async () => {
  try {
    const { value } = await ElMessageBox.prompt(
      $t('user.inputNewNickname'),
      $t('user.changeNickname'),
      {
        confirmButtonText: $t('user.confirm'),
        cancelButtonText: $t('user.cancel'),
        inputPattern: /^.{1,60}$/, // 限制1-60个字符
        inputErrorMessage: $t('user.nicknameLengthError'),
        inputValue: userInfo.nickName // 默认值为当前昵称
      }
    );

    // 用户点击确定后，调用保存方法
    await saveNewNickname(value);
  } catch (error) {
    // 用户点击取消或输入验证失败
    if (error !== 'cancel' && error !== 'close') {
      ElMessage.error($t('user.changeNicknameFailed'));
    }
  }
}

const saveNewNickname = async (newNickname: string) => {
  try {

    // 构造请求数据
    const reqData = {
      TextContent: newNickname
    };

    // 发送请求到后端更新昵称
    const response = await apiClient.post('/User/ChangeUserNickname', reqData);
    if (response.data.result) {
      // 更新本地显示
      userInfo.nickName = newNickname;
      ElMessage.success($t('user.nicknameChangeSuccess'));
    } else {
      ElMessage.error($t('user.nicknameChangeFailed'));
    }
  } catch (error: any) {
    if (error.response?.status === 401) {
      ElMessage.error($t('user.tokenExpired'));
    } else {
      ElMessage.error($t('user.nicknameChangeFailed'));
    }

  }
}

// 添加获取用户信息的方法
const fetchUserInfo = async () => {
  try {
    // 发送请求
    const response = await apiClient.post('/User/GetUserById', null);

    // 处理成功响应
    if (response.data) {
      userInfo.nickName = response.data.result.nickName;
      userInfo.userImage = response.data.result.image;
      userInfo.userType = response.data.result.userType;
    } else {
      ElMessage.error($t('user.getUserInfoFailed'));
    }
  }
  catch (error: any) {
    if (error.response?.status === 401) {
      ElMessage.error($t('user.tokenExpired'));
    } else {
      ElMessage.error($t('user.getUserInfoFailed'));
    }
  }
}

/**
 * 保存用户头像
 */
const saveNewImage = async () => {
  try {
    // 构造符合后端  格式的请求体
    const reqData: text_req = {
      TextContent: userInfo.userImage
    };

    // 发送请求
    const response = await apiClient.post('/User/ChangeUserImage', reqData);
    if (response.data.result) {
      ElMessage.success($t('user.saveAvatarSuccess'));
    }
    else
      ElMessage.error($t('user.saveAvatarFailed'));

  }
  catch (error: any) {
    if (error.response?.status === 401) {
      ElMessage.error($t('user.tokenExpired'));
    } else {
      ElMessage.error($t('user.saveAvatarFailed'));
    }
  }
}


onMounted(() => {
  //获取用户信息
  fetchUserInfo()
})

// 添加 t 函数用于模板中使用
import { useI18n } from 'vue-i18n'
const { t: $t } = useI18n()
</script>

<style scoped>
.text-container {
  display: flex;
  align-items: center;
  gap: 4px;
  font-size: 16px;
  font-weight: bold;
}

.text-content {
  color: #333;
  line-height: 30px;
}

.edit-icon {
  background: none;
  border: none;
  cursor: pointer;
  transition: all 0.2s ease;
  padding: 0;
}

.edit-icon:hover {
  transform: scale(1.1);
}
</style>