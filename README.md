# RetoMagneto
Analizara si una cadena de Adn es Mutante o Es Humano, de esta manera podemos Reclutarlo antes que esos separatistas de Xavier.
Ademas de contar con una Base de Datos que registrara las cadenas analizadas por el Cliente.
# Modo de Uso
Se debe consumir servicio desde cualquier lenguaje de Programación desde la Url: **( http://melfranmaster-001-site1.etempurl.com/ )**
Este  sitio expone 2 Apis:

[POST] **api/mutant

Esta api debe recibir como parametro un objeto JSON con la siguiente Estructura :
{
"dna":["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]
}

tener en cuenta que el sistema esta hecho para secuencias de ADN que contengan las letras (A,T,C,G), en mayusculas por lo que  si existen letras diferentes el sistema , o si la secuencia entregada  no hace referencia a una Matrix de N*N el sistema entregara un codigo de ERROR (FORBIDDEN) y un JSON que describe el error, se  debe tener en cuenta que estas secuencias no entraran bajo el conteo de estadisticas de Mutantes Encontrados ya que No son cadenas Permitidas.

De pasar las Validaciones de manera Correcta el Api Retornara un OK( HttpcodeStatus 200) para confirmar que se encontro un mutante ademas de un objeto con su descripcion. 
{
    "Code": 200,
    "Message": "Mutante",
    "Data": null,
    "Exception": null
}

De lo contrario: el Api Retornara un OK( HttpcodeStatus 403) para confirmar que se encontro un mutante ademas de un objeto con su descripcion. 

{
    "Code": 403,
    "Message": "Humano",
    "Data": null,
    "Exception": null
}

[GET]  **api/stats

Esta Api Trae como resultado un objeto JSON que trae las estadisticas de las cadenas de datos Analizadas
{
    "count_mutant_dna": 2,
    "count_human_dna": 5,
    "Ratio": 0.4
}

# Tener Encuenta

Para Utilizar este servico desde su Cliente debe implementar un metodo con la Firma  **boolean isMutant(String[] dna); [Ejemplo Java]
Si el Servicio regresa un OK sera un **true  como respuesta del Callback de lo contrario se Enviara un **False
