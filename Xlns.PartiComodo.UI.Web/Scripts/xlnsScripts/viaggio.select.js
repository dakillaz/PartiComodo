/// <reference path="../../Scripts/jquery-1.5.1-vsdoc.js" />

function SelectViaggio(IdFlyer, IdViaggio) {
    $('input[type=checkbox]#' + IdViaggio + '').click(function (e) {
        var errMsg = "Errore: aggiornamento flyer fallito";

        if ($(this).is(':checked'))
            errMsg = "Impossibile aggiungere il viaggio al Flyer";
        else
            errMsg = "Impossibile rimuovere il viaggio dal Flyer";

        $.ajax({
            type: 'POST',
            url: "/Flyer/ToggleViaggio",
            cache: false,
            data: { idFlyer: IdFlyer, idViaggio: this.id },
            context: $(this),
            error: function () { alert(errMsg); }
        });
    });
}
