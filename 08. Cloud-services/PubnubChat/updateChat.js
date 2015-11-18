var time = new Date();
var pubnub = PUBNUB({
    subscribe_key: 'sub-c-9e3bccbe-8d58-11e5-bf00-02ee2ddab7fe', // always required
    publish_key: 'pub-c-8f0d4abb-5128-4562-934b-838f657c61d1' // only required if publishing
});

pubnub.subscribe({
    channel: 'chat-channel',
    message: function(m) {
        console.log(m);
        //document.getElementById('messagesArea').innerHTML += m + ' - ' + time.getHours() + ":" + time.getMinutes() + '\n======================\n';
        $('#messagesArea').append(m + ' - ' + time.toLocaleTimeString() + '\n======================\n');
    },
    error: function(error) {
        // Handle error here
        console.log(JSON.stringify(error));
    }
});

$('#sendMsg').on('click', function() {
    pubnub.publish({
        channel: 'chat-channel',
        message: $('#input').val()
    });
    $('#input').val('');
});

$('#input').keypress(function(e) {
    if (e.which == 13) {
        pubnub.publish({
            channel: 'chat-channel',
            message: $('#input').val().trim()
        });
        $('#input').val(null);
    }
});

$("#messagesArea").change(function() {
    scrollToBottom();
});

function scrollToBottom() {
    $('#messagesArea').scrollTop($('#messagesArea')[0].scrollHeight);
}
