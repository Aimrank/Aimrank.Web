<script src="./NotificationsList.ts" lang="ts"></script>
<style src="./NotificationsList.scss" lang="scss" module></style>

<template>
  <div :class="$style.list">
    <transition-group
      tag="div"
      :name="$style.fade"
    >
      <notification
        :class="$style.listItem"
        v-for="notification in notifications"
        :key="notification.id"
        :data="notification"
        @close="hideNotification(notification.id)"
      />
    </transition-group>
  </div>
</template>
