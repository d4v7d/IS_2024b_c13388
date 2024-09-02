
function agregar() {
    var lista = document.getElementById('lista');

    var elementos = lista.getElementsByTagName('li').length;

    var nuevoElemento = document.createElement('li');
    nuevoElemento.textContent = 'Elemento' + (elementos + 1);

    lista.appendChild(nuevoElemento);
}

function cambiarFondo() {
    var body = document.body;

    if (body.style.backgroundColor === 'pink') {
        body.style.backgroundColor = 'white';
    } else {
        body.style.backgroundColor = 'pink';
    }
}

function borrar() {
    var lista = document.getElementById('lista');

    var elementos = lista.getElementsByTagName('li');

    if (elementos.length > 0) {
        lista.removeChild(elementos[elementos.length - 1]);
    }
}
