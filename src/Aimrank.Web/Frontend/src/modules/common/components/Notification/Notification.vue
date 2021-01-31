<script src="./Notification.ts" lang="ts"></script>
<style src="./Notification.scss" lang="scss" module></style>

<template>
  <div
    :class="{
      [$style.notification]: true,
      [$style.notificationSuccess]: data.color === NotificationColor.Success,
      [$style.notificationWarning]: data.color === NotificationColor.Warning,
      [$style.notificationDanger]: data.color === NotificationColor.Danger
    }"
  >
    <div :class="$style.content">
      {{ data.content }}
    </div>
    <div
      :class="$style.close"
      @click="onClose"
    >
      <icon name="times" />
    </div>
  </div>
</template>
