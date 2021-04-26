<script src="./SteamTokens.ts" lang="ts"></script>
<style src="./SteamTokens.scss" lang="scss" module></style>

<template>
  <div>
    <h3 :class="$style.title">{{ $t("admin.views.SteamTokens.title") }}</h3>
    <div v-if="state && state.steamTokens">
      <table :class="$style.table">
        <thead>
          <tr>
            <th>{{ $t("admin.views.SteamTokens.table.pos") }}</th>
            <th>{{ $t("admin.views.SteamTokens.table.token") }}</th>
            <th>{{ $t("admin.views.SteamTokens.table.actions") }}</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(item, index) in state.steamTokens"
            :key="item.token"
          >
            <td>{{ index + 1 }}.</td>
            <td>{{ item.token }}</td>
            <td>
              <div v-if="item.isUsed">{{ $t("admin.views.SteamTokens.active") }}</div>
              <div v-else>
                <base-button
                  @click="onDeleteSteamToken(item.token)"
                  :loading="loadingDelete[item.token]"
                >
                  {{ $t("admin.views.SteamTokens.delete") }}
                </base-button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
      <div :class="$style.form">
        <form-field-input
          :label="$t('admin.views.SteamTokens.addLabel')"
          v-model="token"
        />
        <base-button
          :class="$style.button"
          :disabled="token.length == 0"
          :loading="loadingAdd"
          primary
          @click="onAddSteamToken"
        >
          {{ $t("admin.views.SteamTokens.add") }}
        </base-button>
      </div>
    </div>
  </div>
</template>