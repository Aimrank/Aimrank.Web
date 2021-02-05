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
        <th>Team B</th>
        <th>Score</th>
        <th>Date</th>
      </tr>
      <tr
        v-for="match in matchesWithStatus"
        :key="match.id"
        :class="[
          match.matchResult > 0 ? $style.winner : '',
          match.matchResult < 0 ? $style.looser : ''
        ]"
      >
        <td>{{ match.teamTerrorists[0].username }}</td>
        <td>{{ match.scoreT }}</td>
        <td>{{ match.teamCounterTerrorists[0].username }}</td>
        <td>{{ match.scoreCT }}</td>
        <td>
          {{ new Date(match.finishedAt).toLocaleDateString() }}
        </td>
      </tr>
    </table>
  </div>
</template>
