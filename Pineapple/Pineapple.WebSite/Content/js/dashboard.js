$(function () {
    initTruncateContentHover();
});

function initTruncateContentHover() {
    $(document).tooltip({
        content: function () {
            return $(this).find(".origin-content").html();
        },
        items: ".truncate-content"
    });
}