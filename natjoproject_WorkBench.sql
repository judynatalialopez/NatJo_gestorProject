-- Script SQL para MySQL Workbench
DROP DATABASE IF EXISTS natjoproject;
CREATE DATABASE natjoproject DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE natjoproject;

-- Estructura de tabla para la tabla `administradores`
CREATE TABLE `administradores` (
  `admin_id` int(11) NOT NULL,
  `email` varchar(150) DEFAULT NULL,
  `pwd` varchar(150) DEFAULT NULL,
  `pNombre` varchar(80) DEFAULT NULL,
  `sNombre` varchar(80) DEFAULT NULL,
  `pApellido` varchar(80) DEFAULT NULL,
  `sApellido` varchar(80) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Volcado de datos para la tabla `administradores`
INSERT INTO `administradores` (`admin_id`, `email`, `pwd`, `pNombre`, `sNombre`, `pApellido`, `sApellido`) VALUES
(1, 'natisjcl02@gmail.com', 'Nata', 'Judy', 'Natalia', 'Correa', 'Lopez');
-- Estructura de tabla para la tabla `ciudades`
CREATE TABLE `ciudades` (
  `city_id` int(11) NOT NULL,
  `nombre` varchar(15) NOT NULL,
  `cod_postal` varchar(15) NOT NULL,
  `pais_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Volcado de datos para la tabla `ciudades`
INSERT INTO `ciudades` (`city_id`, `nombre`, `cod_postal`, `pais_id`) VALUES
(1, 'Bogota', '0000', 1);
-- Estructura de tabla para la tabla `comentarios`
CREATE TABLE `comentarios` (
  `texto` varchar(30) DEFAULT NULL,
  `autor_id` varchar(10) DEFAULT NULL,
  `fecha_comentario` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Estructura de tabla para la tabla `dashboards`
CREATE TABLE `dashboards` (
  `dashboard_id` int(11) DEFAULT NULL,
  `user_id` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Estructura de tabla para la tabla `dashboard_proyectos`
CREATE TABLE `dashboard_proyectos` (
  `dashboard_id` int(11) NOT NULL,
  `proj_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Estructura de tabla para la tabla `estados`
CREATE TABLE `estados` (
  `est_id` int(11) NOT NULL,
  `descripcion` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Estructura de tabla para la tabla `estados_task`
CREATE TABLE `estados_task` (
  `estado_id` int(11) NOT NULL,
  `descripcion` varchar(25) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Estructura de tabla para la tabla `miembros`
CREATE TABLE `miembros` (
  `user_id` varchar(10) DEFAULT NULL,
  `rol_id` int(11) DEFAULT NULL,
  `ind_owner` char(1) DEFAULT NULL,
  `ind_admin` char(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Volcado de datos para la tabla `miembros`
INSERT INTO `miembros` (`user_id`, `rol_id`, `ind_owner`, `ind_admin`) VALUES
('1047037318', 2, 'S', 'S'),
('1047037318', 2, 'S', 'S'),
('1047037318', 2, 'S', 'S'),
('1047037318', 2, 'S', 'S');
-- Estructura de tabla para la tabla `paises`
CREATE TABLE `paises` (
  `pais_id` int(11) NOT NULL,
  `nombre` varchar(25) NOT NULL,
  `dominio` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Volcado de datos para la tabla `paises`
INSERT INTO `paises` (`pais_id`, `nombre`, `dominio`) VALUES
(1, 'Colombia', 'Cundinamarca');
-- Estructura de tabla para la tabla `proyectos`
CREATE TABLE `proyectos` (
  `proj_id` int(11) NOT NULL,
  `nombre` varchar(15) DEFAULT NULL,
  `descripcion` varchar(15) DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `f_inicio` datetime DEFAULT NULL,
  `f_terminacion` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Volcado de datos para la tabla `proyectos`
INSERT INTO `proyectos` (`proj_id`, `nombre`, `descripcion`, `team_id`, `f_inicio`, `f_terminacion`) VALUES
(1, 'asd', 'adad', NULL, '2025-05-01 00:00:00', '2025-05-16 00:00:00'),
(2, 'asd', 'adad', NULL, '2025-05-01 00:00:00', '2025-05-16 00:00:00'),
(3, 'adasdd', 'dad', NULL, '2025-05-08 00:00:00', '2025-05-31 00:00:00'),
(4, 'asda', 'adad', NULL, '2025-05-02 00:00:00', '2025-05-31 00:00:00'),
(5, 'adasd', 'adadd', NULL, '2025-05-03 00:00:00', '2025-05-17 00:00:00'),
(6, 'adasd', 'adadd', NULL, '2025-05-03 00:00:00', '2025-05-17 00:00:00'),
(7, 'adasd', 'adadd', NULL, '2025-05-03 00:00:00', '2025-05-17 00:00:00'),
(8, 'adsa', 'asdad', NULL, '2025-05-01 00:00:00', '2025-05-30 00:00:00'),
(9, 'Callofudty', 'juegodedisparo', NULL, '2025-05-15 00:00:00', '2025-05-30 00:00:00'),
(10, 'ada', 'asdad', 10, '2025-05-01 00:00:00', '2025-05-31 00:00:00'),
(11, 'Callofduty', 'LOOOOL', 11, '2025-05-01 00:00:00', '2025-05-31 00:00:00');
-- Estructura de tabla para la tabla `responsables_tarea`
CREATE TABLE `responsables_tarea` (
  `task_id` int(11) NOT NULL,
  `user_id` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Estructura de tabla para la tabla `roles`
CREATE TABLE `roles` (
  `rol_id` int(11) NOT NULL,
  `descripcion` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Volcado de datos para la tabla `roles`
INSERT INTO `roles` (`rol_id`, `descripcion`) VALUES
(2, 'Owner');
-- Estructura de tabla para la tabla `sexos`
CREATE TABLE `sexos` (
  `sx_id` int(11) NOT NULL,
  `descripcion` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Volcado de datos para la tabla `sexos`
INSERT INTO `sexos` (`sx_id`, `descripcion`) VALUES
(1, 'Masculino'),
(2, 'Femenino');
-- Estructura de tabla para la tabla `tasksproject`
CREATE TABLE `tasksproject` (
  `tasks_id` int(11) NOT NULL,
  `titulo` varchar(25) DEFAULT NULL,
  `descripcion` varchar(25) DEFAULT NULL,
  `estado_id` int(11) DEFAULT NULL,
  `f_entrega` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Estructura de tabla para la tabla `teams`
CREATE TABLE `teams` (
  `team_id` int(11) NOT NULL,
  `nombre` varchar(15) DEFAULT NULL,
  `ind_activo` char(1) DEFAULT NULL,
  `proj_id` int(11) DEFAULT NULL,
  `owner_id` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Volcado de datos para la tabla `teams`
INSERT INTO `teams` (`team_id`, `nombre`, `ind_activo`, `proj_id`, `owner_id`) VALUES
(2, 'Equipo de desar', 'N', 2, '1047037318'),
(3, 'Equipo de desar', 'N', 3, '1047037318'),
(4, 'Equipo de desar', 'N', 4, '1047037318'),
(5, 'Equipo de desar', 'N', 5, '1047037318'),
(6, 'Equipo de desar', 'N', 6, '1047037318'),
(7, 'Equipo de desar', 'N', 7, '1047037318'),
(8, 'Equipo de desar', 'N', 8, '1047037318'),
(9, 'Equipo de desar', 'N', 9, '1047037318'),
(10, 'ada', 'N', 10, '1047037318'),
(11, 'Callofduty', 'N', 11, '1047037318');
-- Estructura de tabla para la tabla `team_members`
CREATE TABLE `team_members` (
  `team_id` int(11) DEFAULT NULL,
  `member_id` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Volcado de datos para la tabla `team_members`
INSERT INTO `team_members` (`team_id`, `member_id`) VALUES
(8, '1047037318'),
(9, '1047037318'),
(10, '1047037318'),
(11, '1047037318');
-- Estructura de tabla para la tabla `users`
CREATE TABLE `users` (
  `id` varchar(10) NOT NULL,
  `pNombre` varchar(15) DEFAULT NULL,
  `sNombre` varchar(15) DEFAULT NULL,
  `pApellido` varchar(15) DEFAULT NULL,
  `sApellido` varchar(15) DEFAULT NULL,
  `ndocIdent` varchar(15) DEFAULT NULL,
  `tipo_docIdent` varchar(15) DEFAULT NULL,
  `pais_id` int(11) DEFAULT NULL,
  `ciudad_id` int(11) DEFAULT NULL,
  `sexo_id` int(11) DEFAULT NULL,
  `fNacimiento` datetime DEFAULT NULL,
  `nTelefono1` varchar(10) DEFAULT NULL,
  `nTelefono2` varchar(10) DEFAULT NULL,
  `direccion` varchar(15) DEFAULT NULL,
  `login` varchar(15) DEFAULT NULL,
  `pwd` varchar(15) DEFAULT NULL,
  `email` varchar(15) DEFAULT NULL,
  `indBloqueado` char(1) DEFAULT NULL,
  `indActivo` char(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
-- Volcado de datos para la tabla `users`
INSERT INTO `users` (`id`, `pNombre`, `sNombre`, `pApellido`, `sApellido`, `ndocIdent`, `tipo_docIdent`, `pais_id`, `ciudad_id`, `sexo_id`, `fNacimiento`, `nTelefono1`, `nTelefono2`, `direccion`, `login`, `pwd`, `email`, `indBloqueado`, `indActivo`) VALUES
('1047037318', 'Angel', 'David', 'Acuna', 'Meza', '1047037318', 'TI', 1, 1, 1, '2025-05-01 00:00:00', '3024330119', '3024330119', 'calle249354', 'angel@gmail.com', 'angel@gmail.com', 'angel@gmail.com', 'N', 'S'),
('1047037319', 'Angel', 'Acuna', 'Acuna', 'Meza', '1047037319', 'TI', 1, 1, 2, '2025-05-03 00:00:00', '3024330119', '3024330119', 'calle1043', 'angelo@gmail.co', 'adasd', 'angelo@gmail.co', 'N', 'S');
ALTER TABLE `administradores`
  ADD PRIMARY KEY (`admin_id`);
ALTER TABLE `ciudades`
  ADD PRIMARY KEY (`city_id`),
  ADD KEY `pais_id` (`pais_id`);
ALTER TABLE `comentarios`
  ADD KEY `autor_id` (`autor_id`);
ALTER TABLE `dashboards`
  ADD KEY `dashboard_id` (`dashboard_id`),
  ADD KEY `user_id` (`user_id`);
ALTER TABLE `dashboard_proyectos`
  ADD PRIMARY KEY (`dashboard_id`),
  ADD KEY `proj_id` (`proj_id`);
ALTER TABLE `estados`
  ADD PRIMARY KEY (`est_id`);
ALTER TABLE `estados_task`
  ADD PRIMARY KEY (`estado_id`);
ALTER TABLE `miembros`
  ADD KEY `user_id` (`user_id`),
  ADD KEY `rol_id` (`rol_id`);
ALTER TABLE `paises`
  ADD PRIMARY KEY (`pais_id`);
ALTER TABLE `proyectos`
  ADD PRIMARY KEY (`proj_id`),
  ADD KEY `team_id` (`team_id`);
ALTER TABLE `responsables_tarea`
  ADD PRIMARY KEY (`task_id`,`user_id`),
  ADD KEY `user_id` (`user_id`);
ALTER TABLE `roles`
  ADD PRIMARY KEY (`rol_id`);
ALTER TABLE `sexos`
  ADD PRIMARY KEY (`sx_id`);
ALTER TABLE `tasksproject`
  ADD PRIMARY KEY (`tasks_id`),
  ADD KEY `estado_id` (`estado_id`);
ALTER TABLE `teams`
  ADD PRIMARY KEY (`team_id`),
  ADD KEY `proj_id` (`proj_id`),
  ADD KEY `owner_id` (`owner_id`);
ALTER TABLE `team_members`
  ADD KEY `team_id` (`team_id`),
  ADD KEY `member_id` (`member_id`);
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD KEY `pais_id` (`pais_id`),
  ADD KEY `ciudad_id` (`ciudad_id`),
  ADD KEY `sexo_id` (`sexo_id`);
ALTER TABLE `administradores`
  MODIFY `admin_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
ALTER TABLE `ciudades`
  MODIFY `city_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
ALTER TABLE `dashboard_proyectos`
  MODIFY `dashboard_id` int(11) NOT NULL AUTO_INCREMENT;
ALTER TABLE `estados_task`
  MODIFY `estado_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
ALTER TABLE `paises`
  MODIFY `pais_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
ALTER TABLE `proyectos`
  MODIFY `proj_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
ALTER TABLE `roles`
  MODIFY `rol_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
ALTER TABLE `sexos`
  MODIFY `sx_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
ALTER TABLE `tasksproject`
  MODIFY `tasks_id` int(11) NOT NULL AUTO_INCREMENT;
ALTER TABLE `teams`
  MODIFY `team_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
ALTER TABLE `ciudades`
  ADD CONSTRAINT `ciudades_ibfk_1` FOREIGN KEY (`pais_id`) REFERENCES `paises` (`pais_id`);
ALTER TABLE `comentarios`
  ADD CONSTRAINT `comentarios_ibfk_1` FOREIGN KEY (`autor_id`) REFERENCES `users` (`id`);
ALTER TABLE `dashboards`
  ADD CONSTRAINT `dashboards_ibfk_1` FOREIGN KEY (`dashboard_id`) REFERENCES `dashboard_proyectos` (`dashboard_id`),
  ADD CONSTRAINT `dashboards_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);
ALTER TABLE `dashboard_proyectos`
  ADD CONSTRAINT `dashboard_proyectos_ibfk_1` FOREIGN KEY (`proj_id`) REFERENCES `proyectos` (`proj_id`);
ALTER TABLE `miembros`
  ADD CONSTRAINT `miembros_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  ADD CONSTRAINT `miembros_ibfk_2` FOREIGN KEY (`rol_id`) REFERENCES `roles` (`rol_id`);
ALTER TABLE `proyectos`
  ADD CONSTRAINT `proyectos_ibfk_1` FOREIGN KEY (`team_id`) REFERENCES `team_members` (`team_id`);
ALTER TABLE `responsables_tarea`
  ADD CONSTRAINT `responsables_tarea_ibfk_1` FOREIGN KEY (`task_id`) REFERENCES `tasksproject` (`tasks_id`),
  ADD CONSTRAINT `responsables_tarea_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `miembros` (`user_id`);
ALTER TABLE `tasksproject`
  ADD CONSTRAINT `tasksproject_ibfk_1` FOREIGN KEY (`estado_id`) REFERENCES `estados_task` (`estado_id`);
ALTER TABLE `teams`
  ADD CONSTRAINT `teams_ibfk_2` FOREIGN KEY (`proj_id`) REFERENCES `proyectos` (`proj_id`),
  ADD CONSTRAINT `teams_ibfk_3` FOREIGN KEY (`owner_id`) REFERENCES `users` (`id`);
ALTER TABLE `team_members`
  ADD CONSTRAINT `team_members_ibfk_1` FOREIGN KEY (`member_id`) REFERENCES `miembros` (`user_id`),
  ADD CONSTRAINT `team_members_ibfk_2` FOREIGN KEY (`team_id`) REFERENCES `teams` (`team_id`);
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`pais_id`) REFERENCES `paises` (`pais_id`),
  ADD CONSTRAINT `users_ibfk_2` FOREIGN KEY (`ciudad_id`) REFERENCES `ciudades` (`city_id`),
  ADD CONSTRAINT `users_ibfk_3` FOREIGN KEY (`sexo_id`) REFERENCES `sexos` (`sx_id`);
