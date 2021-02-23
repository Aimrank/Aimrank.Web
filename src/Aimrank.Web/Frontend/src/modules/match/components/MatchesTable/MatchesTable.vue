<script src="./MatchesTable.ts" lang="ts"></script>
<style src="./MatchesTable.scss" lang="scss" module></style>

<template>
  <table :class="$style.table">
    <tr>
      <th>Date</th>
      <th>Map</th>
      <th></th>
      <th>Score</th>
      <th></th>
      <th>Status</th>
      <th>Rating</th>
    </tr>
    <tr
      v-for="match in matches"
      :key="match.id"
    >
      <td>{{ new Date(match.finishedAt).toLocaleDateString() }}</td>
      <td>{{ match.map }}</td>
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
        {{ match.matchPlayerResult.rating }} ({{ match.matchPlayerResult.difference >= 0 ? `+${match.matchPlayerResult.difference}` : match.matchPlayerResult.difference }})
      </td>
    </tr>
  </table>
</template>