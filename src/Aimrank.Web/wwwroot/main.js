(() => {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/game")
    .build();
  
  const start = async () => {
    try {
      await connection.start();
    } catch(error) {
      console.log(error);
    }
  }
  
  const RowPlayer = props => `
    <tr class="row-player">
      <td>${props.steamId}</td>
      <td>${props.name}</td>
      <td>${props.kills}</td>
      <td>${props.assists}</td>
      <td>${props.deaths}</td>
      <td>${props.score}</td>
    </tr>
  `;
  
  const RowScore = (title, score) => `
    <tr class="row-score">
      <td colspan="2">${title}:</td>
      <td colspan="4">${score}</td>
    </tr>
  `;
  
  connection.on("EventReceived", content => {
    const event = JSON.parse(content);
    const time = new Date().toUTCString();

    if (event.name !== "scoreboard_changed") {
      return;
    }
    
    const component = `
      <div class="scoreboard">
        <div class="scoreboard__time">${time}</div>
        <table class="scoreboard__table">
          <tr>
            <th>SteamID64</th>
            <th>Name</th>
            <th>Kills</th>
            <th>Assists</th>
            <th>Deaths</th>
            <th>Score</th>
          </tr>
          ${RowScore("Terrorists", event.data.teamTerrorists.score)}
          ${event.data.teamTerrorists.clients.map(c => RowPlayer(c))}
          ${RowScore("Counter Terrorists", event.data.teamCounterTerrorists.score)}
          ${event.data.teamCounterTerrorists.clients.map(c => RowPlayer(c))}
        </table>
      </div>
    `;
    
    const element = document.getElementById("scoreboard");
    element.innerHTML = component;
  });
  
  start();
})();