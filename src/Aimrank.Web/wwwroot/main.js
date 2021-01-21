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
      <td>${props.name}</td>
      <td>${props.kills}</td>
      <td>${props.deaths}</td>
      <td>-</td>
    </tr>
  `;
  
  const RowScore = (title, score) => `
    <tr class="row-score">
      <td colspan="2">${title}:</td>
      <td colspan="2">${score}</td>
    </tr>
  `;
  
  connection.on("EventReceived", content => {
    const data = JSON.parse(content);
    const time = new Date().toUTCString();
    
    const component = `
      <div class="scoreboard">
        <div class="scoreboard__time">${time}</div>
        <table class="scoreboard__table">
          <tr>
            <th>Name</th>
            <th>Kills</th>
            <th>Deaths</th>
            <th>Score</th>
          </tr>
          ${RowScore("Terrorists", data.scoreboard.teamTerrorists.score)}
          ${data.scoreboard.teamTerrorists.clients.map(c => RowPlayer(c))}
          ${RowScore("Counter Terrorists", data.scoreboard.teamCounterTerrorists.score)}
          ${data.scoreboard.teamCounterTerrorists.clients.map(c => RowPlayer(c))}
        </table>
      </div>
    `;
    
    const element = document.getElementById("scoreboard");
    element.innerHTML = component;
  });
  
  start();
})();