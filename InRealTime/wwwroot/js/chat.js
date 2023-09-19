let currentChatRoom = '';

let connection = new signalR.HubConnectionBuilder().withUrl('/hubs/chat').build();

//const auth = (u) => fetch('/auth?username=', u);

//const send = (message) => connection.send('SendMessage', { message, room: currentChatRoom })
//const create = (room) => fetch('/create?room=' + room)

//const list = () => fetch('/list')
//    .then(r => r.json())
//    .then(r => console.log("rooms", r));

//const join = (room) => connection.start()
//    .then(() => connection.invoke(JoinRoom, { room }))
//    .then((history) => {
//        console.log('message history', history)
//        currentRoom = room
//        connection.on('sendMessage', m => m.console.log(m))
//    });

//const leave = () => connection.send('LeaveRoom', { room: currentChatRoom })
//    .then(() => {
//        currentChatRoom = ''
//        connection.off('sendMessage')
//        return connection.stop()
//    })

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveInbox", function (user, message, sentAt) {
    console.log(`${user} says ${message}`)
    let ul = document.getElementById('msgList')
    let li = document.createElement('li');

    ul.appendChild(li)
    //li.textContent = `${user} says ${message}`;
    li.innerText = `${user}: sent at ${sentAt} says ${message}`;
});

connection.start().then(function () {
    document.getElementById('sendButton').disabled = false;
}).catch(function(err){
    return console.error(err)
})

document.getElementById("sendButton").addEventListener("click", function(event){
    var user = document.getElementById("userInput").value
    var message = document.getElementById("messageInput").value

    connection.invoke("SendInbox", user, message).catch(function(err){
        return console.error(err)
    })

    event.preventDefault();
})

