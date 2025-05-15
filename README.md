# NatJo_gestorProject
Referencias:
https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-class-diagram/

Diagrama de clases: https://app.diagrams.net/#HJohanbeltranramirez%2FDiagramsRepository%2Fdev%2FUninpahu%20Diagrams%2FQuality%20Metricals%2FDiagrama%20sin%20t%C3%ADtulo.drawio#%7B%22pageId%22%3A%22C5RBs43oDa-KdzZeNtuy%22%7D 

Modelo Relacional: https://drive.google.com/file/d/1fpqCGtYLeWzvm4LQfMcK8gG5_IiB7bBn/view?usp=sharing
base de datos:)
CREATE DATABASE natjoproject;

USE natjoproject;


CREATE TABLE paises (
    pais_id varchar(10) PRIMARY KEY not null,
    nombre varchar(25) NOT null,
    dominio varchar(25) not null
    );

CREATE TABLE sexos(
    sx_id varchar(10) PRIMARY KEY  not  null,
    descripcion varchar(30)
    );


CREATE TABLE ciudades (
    city_id VARCHAR(10) PRIMARY KEY NOT null,
    nombre VARCHAR(15) NOT NULL,
    cod_postal varchar(15) NOT null,
    pais_id varchar(10),
    FOREIGN KEY (pais_Id) REFERENCES paises(pais_id)
    );

create table users (
    id varchar(10)  NOT null,
    pNombre varchar (15),
    sNombre varchar (15),
    pApellido varchar (15),
    sApellido varchar (15),
    ndocIdent varchar (15),
    tipo_docIdent varchar (15),
    pais_id varchar (10),
    ciudad_id varchar (10),
    sexo_id varchar (10),
    fNacimiento datetime,
    nTelefono1 varchar (10),
    nTelefono2 varchar (10), 
    direccion varchar(15),
    login varchar (15),
    pwd varchar (15),
    email varchar (15),
    indBloqueado char(1),
    indActivo char(1),
    primary key (id),
    FOREIGN KEY (pais_id) REFERENCES paises(pais_id),
    FOREIGN KEY (ciudad_id) REFERENCES ciudades(city_id),
    FOREIGN KEY (sexo_id) REFERENCES sexos(sx_id)
    );

CREATE TABLE comentarios(
    texto varchar(30),
    autor_id varchar(10),
    fecha_comentario datetime,
    FOREIGN key (autor_id) REFERENCES users(id)
    );

CREATE TABLE estados_task(
    estado_id varchar(10) PRIMARY KEY NOT null,
    descripcion varchar(25)
    );
    
CREATE TABLE roles(
    rol_id varchar(10) PRIMARY KEY NOT null,
    descripcion varchar(30)
    );

CREATE TABLE tasksproject(
    tasks_id varchar(10) NOT null,
    titulo varchar(25),
    descripcion varchar(25),
    estado_id varchar(10),
    f_entrega datetime,
    PRIMARY KEY (tasks_id),
    FOREIGN KEY (estado_id) REFERENCES estados_task(estado_id)
    );


   CREATE TABLE miembros(
    user_id varchar(10),
    rol_id varchar(15),
    ind_owner char,
    ind_admin char,
    FOREIGN key (user_id) REFERENCES users(id),
    FOREIGN key (rol_id) REFERENCES roles(rol_id)
    );

CREATE TABLE team_members(
    team_id varchar(10) NOT null,
    member_id varchar(10),
    PRIMARY KEY (team_id),
    FOREIGN KEY (member_id) REFERENCES miembros(user_id)
    );

CREATE TABLE proyectos(
    proj_id varchar(15) NOT null,
    nombre varchar(15),
    descripcion varchar(15),
    team_id varchar(10),
    f_inicio datetime,
    f_terminacion datetime,
    PRIMARY KEY(proj_id),
    FOREIGN KEY(team_id) REFERENCES team_members(team_id)
    );

CREATE TABLE teams(
    team_id varchar(10),
    nombre varchar(15),
    ind_activo char,
    proj_id varchar(15),
    owner_id varchar(10),
    FOREIGN KEY (team_id) REFERENCES team_members(team_id),
    FOREIGN KEY (proj_id ) REFERENCES proyectos(proj_id),  
    FOREIGN KEY (owner_id) REFERENCES users(id)
    );

CREATE TABLE dashboard_proyectos(
     dashboard_id varchar(10) NOT null,
     proj_id varchar(15),
     PRIMARY KEY(dashboard_id),
     FOREIGN KEY (proj_id ) REFERENCES proyectos(proj_id)
    );

CREATE TABLE dashboards(
    dashboard_id varchar(10),
    user_id varchar(10),
    FOREIGN KEY (dashboard_id) REFERENCES dashboard_proyectos(dashboard_id),
    FOREIGN key  (user_id) REFERENCES users(id)
    );
    
