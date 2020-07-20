let contador = 1;
let valorUnitario = '';
let totalGeral = 0;
let resumoVenda = {};
let listaResumoVenda = [];

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

function apiFinalizarVenda(dados) {
    var urlFinalizarVenda = $('#urlFinalizarVenda').val();
    return $.ajax({
        type: "POST",
        url: urlFinalizarVenda,
        data: { json: JSON.stringify(dados) },
        dataType: 'text',
        contentType: "application/json; charset=utf-8",
        traditional: true
    });
}

function aplicandoMascaras() {
    $('.campoQuantidadeVenda').mask("000.000", { reverse: true });
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

    if (valorUni == '' || quantidade == '' || quantidade == '0')
        return;

    var valorTotal = valorUni * quantidade;
    var texto = valorTotal.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });

    $('.campoValorTotalProdutoVenda').val('');
    $('.campoValorTotalProdutoVenda').val(texto);
}

function btnFinalizarVenda_OnClick() {
    bloqueioDeTela(true);

    var dados = JSON.stringify({ 'view': listaResumoVenda });

    apiFinalizarVenda(dados).done(function (retorno) {
        if (retorno.sucesso) {

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

function ValidarQuantidade() {
    var quantidade = $('.campoQuantidadeVenda').val();

    if (quantidade == '0' || quantidade == '')
        $('.campoQuantidadeVenda').val('1');
}

function VenderProduto_OnClick() {
    var valorTotal = $('.campoValorTotalProdutoVenda').val();
    var nomeProduto = $('.campoNomeProdutoVenda').val();
    var quantidade = $('.campoQuantidadeVenda').val();

    if (valorTotal == '' || nomeProduto == '' || quantidade == '')
        return;

    var addLinhas = $('<tr>');
    var cols = '';

    cols += '<td>' + contador + '</td>';
    cols += '<td>' + nomeProduto.substring(0, 10) + '</td>';
    cols += '<td>' + quantidade + '</td>';
    cols += '<td>' + valorTotal + '</td>';
    cols += '<td>' + ' <button type="button" class="btn btn-light">' +
        '<span class="glyphicon glyphicon-remove"></span>' +
        '</button>' + '</td>';

    addLinhas.append(cols);
    $('#tbRegistroVenda').append(addLinhas);

    resumoVenda = {
        contador: contador,
        nomeProduto: nomeProduto,
        quantidade: quantidade,
        valorTotal: valorTotal
    };

    listaResumoVenda.push(resumoVenda);

    contador++;
}

function documentoVendasReady() {
    aplicandoMascaras();
    $("body").delegate(".campoCodigoVenda", "change", btnBuscarProdutoVenda_OnChange);
    $("body").delegate(".campoQuantidadeVenda", "keyup", btnCalcularValorProduto_keyup);
    $("body").delegate(".campoQuantidadeVenda", "change", ValidarQuantidade);
    $("body").delegate(".venderProduto", "click", VenderProduto_OnClick);
    $("body").delegate(".btnFinalizarVenda", "click", btnFinalizarVenda_OnClick); 

    $('.campoQuantidadeVenda').val('1');
}

$(document).ready(documentoVendasReady);