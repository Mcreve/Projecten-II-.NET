var hijaxView = {
    init: function() {
        $("form").submit(function() {
            $.post(this.action, $(this).serialize(), function(data) {
                $("#search-results").html(data);
            });
            return false;
        });
    }
}

$(function() {
    hijaxView.init();
});