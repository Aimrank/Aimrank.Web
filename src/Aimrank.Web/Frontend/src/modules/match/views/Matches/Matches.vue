<script src="./Matches.ts" lang="ts"></script>
<style src="./Matches.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div :class="$style.header">
      <h3>Matches history</h3>
    </div>
    <table :class="$style.table">
      <tr>
        <th>Date</th>
        <th>Mode</th>
        <th>Team A</th>
        <th>Score</th>
        <th>Team B</th>
        <th>Status</th>
        <th>Rating</th>
      </tr>
      <tr
        v-for="match in matchesWithStatus"
        :key="match.id"
      >
        <td>{{ new Date(match.finishedAt).toLocaleDateString() }}</td>
        <td>{{ ["1 vs 1", "2 vs 2"][match.mode] }}</td>
        <td>{{ match.teamTerrorists[0].username }}</td>
        <td>
          <strong>{{ match.scoreT }} : {{ match.scoreCT }}</strong>
        </td>
        <td>{{ match.teamCounterTerrorists[0].username }}</td>
        <td
          :class="{
            [$style.winner]: match.matchResult > 0,
            [$style.loser]: match.matchResult < 0
          }"
        >
          {{ match.matchResult === 0 ? "DRAW" : (match.matchResult > 0 ? "WIN" : "LOSS") }}
        </td>
        <td
          :class="{
            [$style.winner]: match.matchPlayerResult.difference > 0,
            [$style.loser]: match.matchPlayerResult.difference < 0
          }"
        >
          {{ match.matchPlayerResult.rating }} ({{ match.matchPlayerResult.difference }})
        </td>
      </tr>
    </table>
  </div>
</template>
