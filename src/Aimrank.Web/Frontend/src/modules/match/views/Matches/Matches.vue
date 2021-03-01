<script src="./Matches.ts" lang="ts"></script>
<style src="./Matches.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div :class="$style.section">
      <h3 :class="$style.headline">Stats</h3>
      <table
        v-if="statsState.stats"
        :class="$style.table"
      >
        <tr>
          <th>Matches</th>
          <td>{{ statsState.stats.matchesTotal }}</td>
        </tr>
        <tr>
          <th>Win %</th>
          <td>{{ (statsState.stats.matchesWon / statsState.stats.matchesTotal * 100).toFixed(2) }}</td>
        </tr>
        <tr>
          <th>K/D Ratio</th>
          <td>{{ (statsState.stats.totalKills / statsState.stats.totalDeaths).toFixed(2) }}</td>
        </tr>
        <tr>
          <th>HS %</th>
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
    <div :class="$style.section">
      <h3 :class="$style.headline">Your rating in last 20 matches</h3>
      <rating-chart :matches="matchesState.matches" />
    </div>
    <div :class="$style.section">
      <h3 :class="$style.headline">Last 20 matches</h3>
      <matches-table :matches="matchesState.matches" />
    </div>
  </div>
</template>
