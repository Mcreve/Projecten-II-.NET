var hijaxView = {
    init: function() {
        $("form").submit(function() {
            $.post(this.action, $(this).serialize(), function(data) {
                $("#ajax-result").html(data);
            });
            return false;
        });
    }
}

$(function() {
    hijaxView.init();
});