$(function () {

    $('#addInput').click(function () {
        var maxRow = Number($('#fileInputContain').attr('maxRows')) || 10;
        if ($('#fileInputContain').find('tr').length >= maxRow) return;
        $('#fileInputContain').append($('<tr>').append($('<td>').append($('#fileInputFormat').html())));
    }).hide();

    $('#fileInputContain').on('click', 'button[name=remove]', function () {
        $(this).parents('tr').remove();
        if ($('#fileInputContain').find('tr').length == 0) $('#addInput').click();
    });

    $('#fileInputContain').on('change', 'input[type=file]', function (a, b) {
        if ($(this).attr('hasChanged')) return;
        $('#addInput').click();
        $(this).attr('hasChanged', true);
    });

    $('#addInput').click();

    $('#imageContainer img').lazyload({
        effect: "fadeIn"
    });
});

