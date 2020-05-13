
let listaProdutos = [];
let tabelaProdutos = {};

function apiBuscarProdutos() {
    var urlBuscarProdutos = $('#urlBuscarProdutos').val();
    return $.ajax({
        type: "GET",
        url: urlBuscarProdutos,
        data: {},
        contentType: "application/json; charset=utf-8",
        processData: true,
        traditional: true
    });
}

function apiSalvarProduto(formData) {
    var urlSalvarProduto = $('#urlSalvarProduto').val();
    return $.ajax({
        type: "POST",
        url: urlSalvarProduto,
        data: formData,
        processData: false,
        contentType: false
    });
}

function BuscarProdutos() {
    bloqueioDeTela(true);
    apiBuscarProdutos().done(function (retorno) {
        if (retorno.sucesso) {

            listaProdutos = retorno.dados;

            var table = $('#tbListaProdutos').DataTable();
            table.destroy();

            tabelaProdutos = configuracaoTabelaProdutos();
        }
        else {
            if (retorno.tipo === 'erro') {

            }
            else {

            }
        }
    }).fail(function () { bloqueioDeTela(false); }).always(function () { bloqueioDeTela(false); });
}

function configuracaoTabelaProdutos() {
    let conf = $('#tbListaProdutos').DataTable({
        "language": dataTableLanguage(),
        "aaData": listaProdutos,
        "columns": configuracaoDasColunasDeProdutos()
    });

    return conf;
}

function configuracaoDasColunasDeProdutos() {
    return [
        {
            "data": "codigo",
            className: ""
        }, {
            "data": "nome",
            className: ""
        }, {
            "data": "precoVenda",
            className: ""
        }, {
            "data": "quantidade",
            className: ""
        }];
}

function SalvarProduto_OnClick() {
    bloqueioDeTela(true);
    var files = $('#files')[0].files[0];

    var formData = new FormData();
    formData.append("files", files);
    formData.append("codigo", $('.codigoProduto').val());
    formData.append("nome", $('.nomeProduto').val());
    formData.append("descricao", $('.descricaoProduto').val());
    formData.append("precoCusto", $('.precoCustoProduto').val());
    formData.append("precoVenda", $('.precoVendaProduto').val());
    formData.append("quantidade", $('.quantidadeProduto').val());
    formData.append("observacao", $('.observacaoProduto').val());

    apiSalvarProduto(formData).done(function (retorno) {
        if (retorno.sucesso) {
            abrirDialogSucessoMensagem('O produto foi cadastrado com sucesso!');
            LimparCamposCadastro();
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

function LimparCamposCadastro() {
    $('.codigoProduto').val('');
    $('.nomeProduto').val('');
    $('.descricaoProduto').val('');
    $('.precoCustoProduto').val('');
    $('.precoVendaProduto').val('');
    $('.quantidadeProduto').val('');
    $('.observacaoProduto').val('');
    $('#files').val('');
}

function documentoLoginReady() {

    tabelaProdutos = configuracaoDasColunasDeProdutos();

    $("body").delegate(".salvarProduto", "click", SalvarProduto_OnClick);
    $("body").delegate(".btnConsultaProduto", "click", BuscarProdutos);

    BuscarProdutos();
}

$(document).ready(documentoLoginReady);