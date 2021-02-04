<script src="./Matches.ts" lang="ts"></script>
<style src="./Matches.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div :class="$style.header">
      <h3>Matches history</h3>
    </div>
    <table :class="$style.table">
      <tr>
        <th>Team A</th>
        <th>Score</th>
        <th></th>
        <th>Score</th>
        <th>Team B</th>
        <th>Date</th>
      </tr>
    </table>
  </div>
</template>
