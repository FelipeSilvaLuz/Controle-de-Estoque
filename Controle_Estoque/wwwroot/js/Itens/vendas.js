let contador = 1;
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

function btnCalcularValorProduto_keyup() {
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

function ValidarQuantidade() {
    var quantidade = $('.campoQuantidadeVenda').val();

    if (quantidade == '0' || quantidade == '')
        $('.campoQuantidadeVenda').val('1');
}

function VenderProduto_OnClick() {
    var valorProduto = $('.campoValorTotalProdutoVenda').val();
    var nomeProduto = $('.campoNomeProdutoVenda').val();
    var quantidade = $('.campoQuantidadeVenda').val();

    if (valorProduto == '' || nomeProduto == '' || quantidade == '')
        return;

    var addLinhas = $('<tr>');
    var cols = '';

    cols += '<td>' + contador + '</td>';
    cols += '<td>' + nomeProduto.substring(0, 10) + '</td>';
    cols += '<td>' + quantidade + '</td>';
    cols += '<td>' + valorProduto + '</td>';
    cols += '<td>' + ' <button type="button" class="btn btn-light">' +
        '<span class="glyphicon glyphicon-remove"></span>' +
        '</button>' + '</td>';

    contador++;

    addLinhas.append(cols);
    $('#tbRegistroVenda').append(addLinhas);
}

function documentoVendasReady() {
    aplicandoMascaras();
    $("body").delegate(".campoCodigoVenda", "change", btnBuscarProdutoVenda_OnChange);
    $("body").delegate(".campoQuantidadeVenda", "keyup", btnCalcularValorProduto_keyup);
    $("body").delegate(".campoQuantidadeVenda", "change", ValidarQuantidade);
    $("body").delegate(".venderProduto", "click", VenderProduto_OnClick);

    $('.campoQuantidadeVenda').val('1');
}

$(document).ready(documentoVendasReady);