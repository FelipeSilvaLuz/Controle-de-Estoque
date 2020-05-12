function bloqueioDeTela(bloquear) {
    if (bloquear === true) {
        var data = $('#domMessage').data();

        if (data && data["blockUI.isBlocked"] != 1)
            $.blockUI({ message: $('#domMessage') });
    }
    else {
        $.unblockUI();
    }
}