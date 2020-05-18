
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

function apiImageToBase64(formData) {
    var urlImageToBase64 = $('#urlImageToBase64').val();
    return $.ajax({
        url: urlImageToBase64,
        data: formData,
        processData: false,
        contentType: false,
        type: "POST"
    });
}

function apiBuscarPorCodigo(codigo) {
    var urlBuscarPorCodigo = $('#urlBuscarPorCodigo').val();
    return $.ajax({
        type: "GET",
        url: urlBuscarPorCodigo + "?codigoProduto=" + codigo,
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

function aplicandoMascaras() {
    $('.quantidadeProduto').mask("000.000.000", { reverse: true });
    $('.precoCustoProduto').mask("000.000.000,00", { reverse: true });
    $('.precoVendaProduto').mask("000.000.000,00", { reverse: true });
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
            "data": "produtoId",
            className: "custom-center col-produtoId-produto"
        }, {
            "data": "codigo",
            className: "custom-left col-codigo-produto"
        }, {
            "data": "nome",
            className: "custom-left col-nome-produto"
        }, {
            "data": "precoVendaExibir",
            className: "custom-right col-valor-venda-produto"
        }, {
            "data": "quantidade",
            className: "custom-right col-quantidade-produto"
        }, {
            "data": null,
            className: "custom-center col-acoes-produto",
            "orderable": false,
            render: configuracaoDasColunasProdutosRenderBotoes
        }];
}

function configuracaoDasColunasProdutosRenderBotoes(data, type, row) {
    var retorno = '';

    retorno += ' <button type="button" class="btn btn-primary btn-xs" data-numero="' + row.codigo
        + '" title="Visualizar"><span class="glyphicon glyphicon-search"></span></button>';

    retorno += ' <button type="button" class="btn btn-primary btn-xs" data-numero="' + row.codigo
        + '" title="Download"><span class="glyphicon glyphicon-cloud-download"></span></button>';

    return retorno;
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

function BuscarPorCodigo() {

    let codigo = $('.codigoProduto').val();

    apiBuscarPorCodigo(codigo).done(function (retorno) {
        if (retorno.sucesso) {
            $('.nomeProduto').val(retorno.dados.nome);
            $('.descricaoProduto').val(retorno.dados.descricao);
            $('.precoCustoProduto').val(retorno.dados.precoCusto);
            $('.precoVendaProduto').val(retorno.dados.precoVenda);
            $('.quantidadeProduto').val(retorno.dados.quantidade);
            $('.observacaoProduto').val(retorno.dados.observacao);

            $('.previewImage').attr('src', '');
            $('.previewImage').attr('src', 'data:image/jpeg;base64, ' + retorno.dados.base64);

            $('.salvarProduto').text('Alterar Cadastro');
        }
        else {
            $('.nomeProduto').val('');
            $('.descricaoProduto').val('');
            $('.precoCustoProduto').val('');
            $('.precoVendaProduto').val('');
            $('.quantidadeProduto').val('');
            $('.observacaoProduto').val('');
            $('.previewImage').attr('src', '');

            $('.salvarProduto').text('Confirmar Cadastro');
        }
    });
}

function PreviewFotoAnexada_OnChange() {

    var files = $('#files')[0].files[0];

    var formData = new FormData();
    formData.append("files", files);

    apiImageToBase64(formData).done(function (retorno) {
        if (retorno.sucesso) {

            $('.previewImage').attr('src', '');
            $('.previewImage').attr('src', 'data:image/jpeg;base64, ' + retorno.dados);

        }
        else {
            if (retorno.tipo === 'erro') {

            }
            else {

            }
        }
    });
}


function documentoLoginReady() {

    tabelaProdutos = configuracaoDasColunasDeProdutos();

    $("body").delegate(".salvarProduto", "click", SalvarProduto_OnClick);
    $("body").delegate(".btnConsultaProduto", "click", BuscarProdutos);
    $("body").delegate(".codigoProduto", "change", BuscarPorCodigo);
    $("body").delegate(".anexarFoto", "change", PreviewFotoAnexada_OnChange);

    BuscarProdutos();
    aplicandoMascaras();
}

$(document).ready(documentoLoginReady);