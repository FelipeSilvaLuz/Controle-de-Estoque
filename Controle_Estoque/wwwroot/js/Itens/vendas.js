
let valorUnitario = '';

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

            var valor = retorno.valorUnitario.toLocaleString("pt-BR", { style: "currency", currency: "BRL" })
            valorUnitario = retorno.valorUnitario;

            $('.campoValorUnitarioVenda').val(valor);
            $('.campoValorTotalProdutoVenda').val(valor);
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

function btnCalcularValorProduto_OnChange() {
    var valorUni = valorUnitario;
    var quantidade = $('.campoQuantidadeVenda').val();

    if (valorUni == '' || quantidade == '')
        return;

    var valorTotal = valorUni * quantidade;
    var texto = valorTotal.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });

    $('.campoValorTotalProdutoVenda').val('');
    $('.campoValorTotalProdutoVenda').val(texto);
}

function aplicandoMascaras() {
    $('.campoQuantidadeVenda').mask("000.000", { reverse: true });
}

function documentoVendasReady() {
    aplicandoMascaras();
    $("body").delegate(".campoCodigoVenda", "change", btnBuscarProdutoVenda_OnChange);
    $("body").delegate(".campoQuantidadeVenda", "keyup", btnCalcularValorProduto_OnChange);
}

$(document).ready(documentoVendasReady);