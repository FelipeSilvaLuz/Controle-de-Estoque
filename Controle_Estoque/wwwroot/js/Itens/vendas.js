
function apiBuscarporCodigoParaVenda(codigo) {
    var urlBuscarPorCodigoParaVenda = $('#urlBuscarPorCodigoParaVenda').val();
    return $.ajax({
        type: "GET",
        url: urlBuscarPorCodigoParaVenda + "?codigoProduto=" + codigo,
        data: {},
        contentType: "application/json; charset=utf-8",
        processData: true,
        traditional: true
    });
}

function btnBuscarProdutoVenda_OnChange() {
    bloqueioDeTela(true);
    var codigo = $('.campoCodigoVenda').val();

    apiBuscarporCodigoParaVenda(codigo).done(function (retorno) {
        if (retorno.sucesso) {
            $('.campoValorUnitarioVenda').val(retorno.valorUnitario);
            $('.campoValorTotalProdutoVenda').val(retorno.valorUnitario);
            $('.campoNomeProdutoVenda').val(retorno.nomeProduto);
            $('.campoObservacaoVenda').val(retorno.observacao);
        }
        else {
            if (retorno.tipo === 'erro') {
                abrirDialogErroMensagem(retorno.mensagens);
            }
            else {
                abrirDialogAlertaListaMensagem(retorno.mensagens);
            }
        }
    }).fail(function () { bloqueioDeTela(false); }).always(function () { bloqueioDeTela(false); });
}


function documentoVendasReady() {
    
    $("body").delegate(".campoCodigoVenda", "change", btnBuscarProdutoVenda_OnChange);

}

$(document).ready(documentoVendasReady);