<script src="./Matches.ts" lang="ts"></script>
<style src="./Matches.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <select
      :class="$style.select"
      :disabled="state.isLoading"
      v-model="state.mode"
    >
      <option :value="MatchMode.OneVsOne">1 vs 1</option>
      <option :value="MatchMode.TwoVsTwo">2 vs 2</option>
    </select>
    <div :class="$style.section">
      <h3 :class="$style.headline">Your rating in last 20 matches</h3>
      <rating-chart :matches="matchesWithStatus" />
    </div>
    <div :class="$style.section">
      <h3 :class="$style.headline">Last 20 matches</h3>
      <matches-table :matches="matchesWithStatus" />
    </div>
  </div>
</template>
