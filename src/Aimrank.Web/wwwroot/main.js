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
  
  connection.on("MessageReceived", content => {
    const time = new Date().toUTCString();
    const ul = document.getElementById("events");
    const li = document.createElement("li");
    li.innerHTML = `<span>${time}</span><code><pre>${content}</pre></code>`;
    ul.appendChild(li);
  });
  
  connection.on("ServerMessageReceived", content => {
    const pre = document.getElementById("logs");
    pre.innerHTML += "\n";
    pre.innerHTML += content;
  });
  
  start();
})();