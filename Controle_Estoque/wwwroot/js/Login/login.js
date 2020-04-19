
function apiValidarAcesso(dados) {
    var urlValidarAcesso = $('#urlValidarAcesso').val();
    return $.ajax({
        type: "POST",
        url: urlValidarAcesso,
        data: JSON.stringify(dados),
        contentType: "application/json; charset=utf-8",
        processData: true,
        traditional: true
    });
}

function ValidarAcesso_OnClick() {

    let dados = {
        usuario: $('.LoginUsuario').val(),
        senha: $('.SenhaUsuario').val()
    };

    apiValidarAcesso(dados).done(function (retorno) {
        if (retorno.sucesso) {

        }
        else {

        }
    });
}


function documentoLoginReady() {

    $("body").delegate(".btnAcessarSistema", "click", ValidarAcesso_OnClick);
}

$(document).ready(documentoLoginReady);