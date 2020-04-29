
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




function documentoLoginReady() {
    BuscarProdutos();

    $("body").delegate(".produtosClick", "click", ValidarAcesso_OnClick);
    

}

$(document).ready(documentoLoginReady);