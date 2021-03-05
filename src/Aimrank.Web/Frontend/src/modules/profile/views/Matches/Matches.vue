<script src="./Matches.ts" lang="ts"></script>
<style src="./Matches.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div :class="$style.section">
      <h3 :class="$style.headline">{{ $t("profile.views.Matches.stats.title") }}</h3>
      <table
        v-if="statsState.stats"
        :class="$style.table"
      >
        <tr>
          <th>{{ $t("profile.views.Matches.stats.matches") }}</th>
          <td>{{ statsState.stats.matchesTotal }}</td>
        </tr>
        <tr>
          <th>{{ $t("profile.views.Matches.stats.winPercentage") }}</th>
          <td>{{ (statsState.stats.matchesWon / statsState.stats.matchesTotal * 100).toFixed(2) }}</td>
        </tr>
        <tr>
          <th>{{ $t("profile.views.Matches.stats.kdRatio") }}</th>
          <td>{{ (statsState.stats.totalKills / statsState.stats.totalDeaths).toFixed(2) }}</td>
        </tr>
        <tr>
          <th>{{ $t("profile.views.Matches.stats.hsPercentage") }}</th>
          <td>{{ (statsState.stats.totalHs / statsState.stats.totalKills * 100).toFixed(2) }}</td>
        </tr>
      </table>
    </div>
    <select
      :class="$style.select"
      :disabled="isLoading"
      v-model="matchesState.mode"
    >
      <option :value="MatchMode.OneVsOne">1 vs 1</option>
      <option :value="MatchMode.TwoVsTwo">2 vs 2</option>
    </select>
    <template v-if="matchesState.matches.length">
      <div :class="$style.section">
        <h3 :class="$style.headline">{{ $t("profile.views.Matches.ratingTitle", [matchesState.matches.length]) }}</h3>
        <rating-chart :matches="matchesState.matches" />
      </div>
      <div :class="$style.section">
        <h3 :class="$style.headline">{{ $t("profile.views.Matches.matchesTitle", [matchesState.matches.length]) }}</h3>
        <matches-table :matches="matchesState.matches" />
      </div>
    </template>
  </div>
</template>
