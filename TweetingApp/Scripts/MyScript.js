
function SendRequest(url, data, callback) {
    $.ajax({
        method: "POST",
        url: url,
        data: data,
        success: function (rooms) {
            location.reload();
        }
    })
    .error(function (err, err2, err3) {
        console.error(err);
        console.error(err2);
        console.error(err3);
    });
}


function SendRequest2(url,data, callback) {
    $ajax({
        method: "POST",
        url: url,
        data: data


    })
    success:JSON.stringify(data)
    .error(function (err, err2, err3) {
        console.error(err);
    });
}

function ReplyModal(tweetId, userName, tweetText,userImage) {
    $("#Modal").modal("show");
    $("#ReplayUserName").html(userName);
    $("#ReplaytweetText").html(tweetText);
    $("#txtReplayTweetId").val(tweetId);
    $("#ReplayuserImage").html(userImage);
}
function ReplyTweet() {
    var id = $("#txtReplayTweetId").val();
    var text = $("#txtReplayTweet").val();
   
    SendRequest("/Home/Tweet_Yanitla?twitId=" + id, null, function (data) {
        alert("Tweet gönderildi.");
        $("#Modal").modal("hide");

        $("#txtReplayTweetId").val("");
        $("#txtReplayTweet").val("");
    });
   
}


