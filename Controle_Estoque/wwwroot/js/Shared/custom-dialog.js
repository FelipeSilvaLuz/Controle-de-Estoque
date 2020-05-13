var funcaoAoFechar = null;

function abrirDialogAlertaListaMensagem(mensagens) {
    adicionarTextosNaDiv($('#alertText'), mensagens);

    $('#dialogWarning').removeClass('invisible').addClass('visible');
}

function abrirDialogAlertaMensagem(mensagem) {
    $('#alertText').text(mensagem);
    $('#dialogWarning').removeClass('invisible').addClass('visible');
}

function abrirDialogErroListaMensagem(mensagens) {
    adicionarTextosNaDiv($('#errorText'), mensagens);

    $('#dialogError').removeClass('invisible').addClass('visible');
}

function abrirDialogErroMensagem(mensagem) {
    if (!mensagem || mensagem === '') {
        mensagem = 'Erro ao executar ação, tente novamente ou entre em contato com o administrador.';
    }

    $('#errorText').text(mensagem);
    $('#dialogError').removeClass('invisible').addClass('visible');
}

function abrirDialogSucessoListaMensagem(mensagens) {
    adicionarTextosNaDiv($('#successText'), mensagens);

    $('#dialogSuccess').removeClass('invisible').addClass('visible');
}

function abrirDialogSucessoMensagem(mensagem, acaoAoFechar) {

    if (acaoAoFechar || acaoAoFechar !== null)
        funcaoAoFechar = acaoAoFechar;

    $('#successText').text(mensagem);
    $('#dialogSuccess').removeClass('invisible').addClass('visible');
}

function adicionarTextosNaDiv(elemento, textos) {
    elemento.text('');
    for (var i = 0; i < textos.length; i++) {
        if (i > 0) {
            elemento.append('<br />');
        }
        elemento.append(textos[i]);
    }
}

function fecharDialog(elemento) {
    elemento.removeClass('visible').addClass('invisible');

    if (funcaoAoFechar || funcaoAoFechar !== null)
        funcaoAoFechar();
}

function fecharDialogAlerta() {
    fecharDialog($('#dialogWarning'));
}

function fecharDialogErro() {
    fecharDialog($('#dialogError'));
}

function fecharDialogSucesso() {
    fecharDialog($('#dialogSuccess'));
}

$('#btnCloseDialogWarning').on('click', fecharDialogAlerta);
$('#btnCloseDialogError').on('click', fecharDialogErro);
$('#btnCloseDialogSuccess').on('click', fecharDialogSucesso);