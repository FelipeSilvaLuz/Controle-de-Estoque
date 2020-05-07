
function apiBuscarProdutos(dados) {
    var urlBuscarProdutos = $('#urlBuscarProdutos').val();
    return $.ajax({
        type: "POST",
        url: urlBuscarProdutos,
        data: JSON.stringify(dados),
        contentType: "application/json; charset=utf-8",
        processData: false,
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
    apiBuscarProdutos().done(function (retorno) {
        if (retorno.sucesso) {

        }
        else {
            if (retorno.tipo === 'erro') {

            }
            else {

            }
        }
    });
}

function SalvarProduto_OnClick() {

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

        }
    });
}

function documentoLoginReady() {
    BuscarProdutos();

    $("body").delegate(".salvarProduto", "click", SalvarProduto_OnClick);

}

$(document).ready(documentoLoginReady);