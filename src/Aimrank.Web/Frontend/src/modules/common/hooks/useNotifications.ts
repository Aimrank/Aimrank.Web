import { readonly, ref } from "vue";
import { v4 as uuid } from "uuid";

export enum NotificationColor {
  Success,
  Warning,
  Danger
}

export interface INotification {
  id: string;
  content: string;
  timeout: number;
  color: NotificationColor;
}

export interface INotificationOptions {
  content: string;
  timeout: number;
  color: NotificationColor;
}

const timeouts = new Map<string, any>();

const notifications = ref<INotification[]>([]);

const showNotification = (options: INotificationOptions) => {
  const notification: INotification = {
    ...options,
    id: uuid()
  };

  notifications.value = [
    ...notifications.value,
    {
      ...notification
    }
  ];

  if (notification.timeout !== -1) {
    const timeout = setTimeout(
      () => {
        hideNotification(notification.id);
      },
      notification.timeout
    );

    timeouts.set(notification.id, timeout);
  }
}

const hideNotification = (id: string) => {
  notifications.value = notifications.value.filter(n => n.id !== id);

  if (timeouts.has(id)) {
    timeouts.delete(id);
  }
}

const success = (content: string, timeout = -1) => {
  showNotification({
    color: NotificationColor.Success,
    content,
    timeout
  });
}

const warning = (content: string, timeout = -1) => {
  showNotification({
    color: NotificationColor.Warning,
    content,
    timeout
  });
}

const danger = (content: string, timeout = -1) => {
  showNotification({
    color: NotificationColor.Danger,
    content,
    timeout
  });
}

export const useNotifications = () => ({
  notifications: readonly(notifications),
  showNotification,
  hideNotification,
  success,
  warning,
  danger
});
