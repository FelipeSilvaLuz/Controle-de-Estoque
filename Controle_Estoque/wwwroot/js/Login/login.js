
function apiValidarAcesso(dados) {
    var urlValidarAcesso = $('#urlValidarAcesso').val();
    return $.ajax({
        type: "POST",
        url: urlValidarAcesso,
        data: JSON.stringify(dados),
        contentType: "application/json; charset=utf-8",
        processData: false,
        traditional: true
    });
}

function ValidarAcesso_OnClick() {

    $('.dangerValidacaoLogin').removeClass('visible');
    $('.alertaValidacaoLogin').removeClass('visible');
    $('.dangerValidacaoLogin').addClass('invisible');
    $('.alertaValidacaoLogin').addClass('invisible');
    $('.dangerValidacaoLogin').text('');
    $('.alertaValidacaoLogin').text('');

    let dados = {
        email: $('.LoginUsuario').val(),
        senha: $('.SenhaUsuario').val()
    };

    apiValidarAcesso(dados).done(function (retorno) {
        if (retorno.sucesso) {
            $(window.document.location).attr('href', 'https://localhost:44301/listagem');
        }
        else {
            if (retorno.tipo === 'erro') {
                $('.dangerValidacaoLogin').removeClass('invisible');
                $('.dangerValidacaoLogin').addClass('visible');
                $('.dangerValidacaoLogin').text(retorno.mensagem);
            }
            else {
                $('.alertaValidacaoLogin').removeClass('invisible');
                $('.alertaValidacaoLogin').addClass('visible');
                $('.alertaValidacaoLogin').text(retorno.mensagem);
            }
        }
    });
}


function documentoLoginReady() {

    $("body").delegate(".btnAcessarSistema", "click", ValidarAcesso_OnClick);
}

$(document).ready(documentoLoginReady);