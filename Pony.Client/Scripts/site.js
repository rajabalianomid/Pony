function moveFunc(id, direction) {
    var model = { MazeId: id, Direction: direction };
    addAntiForgeryToken(model);
    var url = '/game/move';
    if (direction == 5) {
        url = '/game/automove';
    }
    $.ajax({
        cache: false,
        url: url,
        type: 'post',
        data: model,
        dataType: 'json',
        success: function (data) {
            if (data.State != 'over' && data.State != 'won') {
                $("#print").html(data.Data);
                if (data.IsAuto == true) {
                    moveFunc(data.MazeId, 5);
                }
            }
            else {
                alert(data.State);
                window.location.replace('/home');
            }
        }
    });
};
function print(id) {
    var model = { Id: id };
    $.ajax({
        cache: false,
        url: '/game/print',
        type: 'post',
        data: model,
        dataType: 'json',
        success: function (data) {
            $("#print").html(data.Data);
            if (data.MazeId != null) {
                moveFunc(data.StateResult, 5);
            }
        }
    });
};
function addAntiForgeryToken(data) {
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};
