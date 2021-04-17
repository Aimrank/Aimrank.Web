<script src="./BaseDialog.ts" lang="ts"></script>
<style src="./BaseDialog.scss" lang="scss" module></style>

<template>
  <span
    v-if="state.isVisible"
    :class="$style.teleport"
  >
    <teleport to="#dialogs">
      <div
        :class="{
          [$style.fadeEnterActive]: state.isEnterActive,
          [$style.fadeLeaveActive]: state.isLeaveActive,
          [$style.fadeEnterFrom]: state.isEntering,
          [$style.fadeLeaveTo]: state.isLeaving
        }"
      >
        <div :class="$style.overlay" />
        <div
          :class="$style.wrapper"
          @click="onBackgroundClick"
        >
          <div
            ref="container"
            :class="$style.container"
            :style="{
              '--container-width': width ? `${width}px` : '',
              '--container-width-min': minWidth ? `${minWidth}px` : '',
              '--container-width-max': maxWidth ? `${maxWidth}px` : ''
            }"
          >
            <div
              v-if="$slots.header"
              :class="$style.header"
            >
              <slot name="header" />
              <icon
                v-if="!hideCloseIcon"
                :class="$style.headerCloseButton"
                name="times"
                @click="onCloseClick"
              />
            </div>
            <div :class="$style.content">
              <slot />
            </div>
            <div
              v-if="$slots.footer"
              :class="$style.footer"
            >
              <slot name="footer" />
            </div>
          </div>
        </div>
      </div>
    </teleport>
  </span>
</template>
