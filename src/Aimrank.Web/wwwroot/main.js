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
  
  connection.on("EventReceived", content => {
    const time = new Date().toUTCString();
    const ul = document.getElementById("events");
    const li = document.createElement("li");
    li.innerHTML = `<span>${time}</span><code><pre>${content}</pre></code>`;
    ul.appendChild(li);
  });
  
  start();
})();