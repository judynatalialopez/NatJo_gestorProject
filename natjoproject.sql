-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 17-05-2025 a las 20:57:18
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `natjoproject`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ciudades`
--

CREATE TABLE `ciudades` (
  `city_id` int(11) NOT NULL,
  `nombre` varchar(15) NOT NULL,
  `cod_postal` varchar(15) NOT NULL,
  `pais_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `ciudades`
--

INSERT INTO `ciudades` (`city_id`, `nombre`, `cod_postal`, `pais_id`) VALUES
(1, 'Bogota', '0000', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `comentarios`
--

CREATE TABLE `comentarios` (
  `texto` varchar(30) DEFAULT NULL,
  `autor_id` varchar(10) DEFAULT NULL,
  `fecha_comentario` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `dashboards`
--

CREATE TABLE `dashboards` (
  `dashboard_id` int(11) DEFAULT NULL,
  `user_id` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `dashboard_proyectos`
--

CREATE TABLE `dashboard_proyectos` (
  `dashboard_id` int(11) NOT NULL,
  `proj_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `estados_task`
--

CREATE TABLE `estados_task` (
  `estado_id` int(11) NOT NULL,
  `descripcion` varchar(25) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `miembros`
--

CREATE TABLE `miembros` (
  `user_id` varchar(10) DEFAULT NULL,
  `rol_id` int(11) DEFAULT NULL,
  `ind_owner` char(1) DEFAULT NULL,
  `ind_admin` char(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `paises`
--

CREATE TABLE `paises` (
  `pais_id` int(11) NOT NULL,
  `nombre` varchar(25) NOT NULL,
  `dominio` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `paises`
--

INSERT INTO `paises` (`pais_id`, `nombre`, `dominio`) VALUES
(1, 'Colombia', 'Cundinamarca');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proyectos`
--

CREATE TABLE `proyectos` (
  `proj_id` int(11) NOT NULL,
  `nombre` varchar(15) DEFAULT NULL,
  `descripcion` varchar(15) DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `f_inicio` datetime DEFAULT NULL,
  `f_terminacion` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roles`
--

CREATE TABLE `roles` (
  `rol_id` int(11) NOT NULL,
  `descripcion` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `sexos`
--

CREATE TABLE `sexos` (
  `sx_id` int(11) NOT NULL,
  `descripcion` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `sexos`
--

INSERT INTO `sexos` (`sx_id`, `descripcion`) VALUES
(1, 'Masculino'),
(2, 'Femenino');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tasksproject`
--

CREATE TABLE `tasksproject` (
  `tasks_id` int(11) NOT NULL,
  `titulo` varchar(25) DEFAULT NULL,
  `descripcion` varchar(25) DEFAULT NULL,
  `estado_id` int(11) DEFAULT NULL,
  `f_entrega` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `teams`
--

CREATE TABLE `teams` (
  `team_id` int(11) DEFAULT NULL,
  `nombre` varchar(15) DEFAULT NULL,
  `ind_activo` char(1) DEFAULT NULL,
  `proj_id` int(11) DEFAULT NULL,
  `owner_id` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `team_members`
--

CREATE TABLE `team_members` (
  `team_id` int(11) NOT NULL,
  `member_id` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `users`
--

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

--
-- Volcado de datos para la tabla `users`
--

INSERT INTO `users` (`id`, `pNombre`, `sNombre`, `pApellido`, `sApellido`, `ndocIdent`, `tipo_docIdent`, `pais_id`, `ciudad_id`, `sexo_id`, `fNacimiento`, `nTelefono1`, `nTelefono2`, `direccion`, `login`, `pwd`, `email`, `indBloqueado`, `indActivo`) VALUES
('1047037318', 'Angel', 'David', 'Acuna', 'Meza', '1047037318', 'TI', 1, 1, 1, '2025-05-01 00:00:00', '3024330119', '3024330119', 'calle249354', 'angel@gmail.com', 'angel@gmail.com', 'angel@gmail.com', 'N', 'S');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `ciudades`
--
ALTER TABLE `ciudades`
  ADD PRIMARY KEY (`city_id`),
  ADD KEY `pais_id` (`pais_id`);

--
-- Indices de la tabla `comentarios`
--
ALTER TABLE `comentarios`
  ADD KEY `autor_id` (`autor_id`);

--
-- Indices de la tabla `dashboards`
--
ALTER TABLE `dashboards`
  ADD KEY `dashboard_id` (`dashboard_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indices de la tabla `dashboard_proyectos`
--
ALTER TABLE `dashboard_proyectos`
  ADD PRIMARY KEY (`dashboard_id`),
  ADD KEY `proj_id` (`proj_id`);

--
-- Indices de la tabla `estados_task`
--
ALTER TABLE `estados_task`
  ADD PRIMARY KEY (`estado_id`);

--
-- Indices de la tabla `miembros`
--
ALTER TABLE `miembros`
  ADD KEY `user_id` (`user_id`),
  ADD KEY `rol_id` (`rol_id`);

--
-- Indices de la tabla `paises`
--
ALTER TABLE `paises`
  ADD PRIMARY KEY (`pais_id`);

--
-- Indices de la tabla `proyectos`
--
ALTER TABLE `proyectos`
  ADD PRIMARY KEY (`proj_id`),
  ADD KEY `team_id` (`team_id`);

--
-- Indices de la tabla `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`rol_id`);

--
-- Indices de la tabla `sexos`
--
ALTER TABLE `sexos`
  ADD PRIMARY KEY (`sx_id`);

--
-- Indices de la tabla `tasksproject`
--
ALTER TABLE `tasksproject`
  ADD PRIMARY KEY (`tasks_id`),
  ADD KEY `estado_id` (`estado_id`);

--
-- Indices de la tabla `teams`
--
ALTER TABLE `teams`
  ADD KEY `team_id` (`team_id`),
  ADD KEY `proj_id` (`proj_id`),
  ADD KEY `owner_id` (`owner_id`);

--
-- Indices de la tabla `team_members`
--
ALTER TABLE `team_members`
  ADD PRIMARY KEY (`team_id`),
  ADD KEY `member_id` (`member_id`);

--
-- Indices de la tabla `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD KEY `pais_id` (`pais_id`),
  ADD KEY `ciudad_id` (`ciudad_id`),
  ADD KEY `sexo_id` (`sexo_id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `ciudades`
--
ALTER TABLE `ciudades`
  MODIFY `city_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `dashboard_proyectos`
--
ALTER TABLE `dashboard_proyectos`
  MODIFY `dashboard_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `estados_task`
--
ALTER TABLE `estados_task`
  MODIFY `estado_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `paises`
--
ALTER TABLE `paises`
  MODIFY `pais_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `proyectos`
--
ALTER TABLE `proyectos`
  MODIFY `proj_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `roles`
--
ALTER TABLE `roles`
  MODIFY `rol_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `sexos`
--
ALTER TABLE `sexos`
  MODIFY `sx_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `tasksproject`
--
ALTER TABLE `tasksproject`
  MODIFY `tasks_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `team_members`
--
ALTER TABLE `team_members`
  MODIFY `team_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `ciudades`
--
ALTER TABLE `ciudades`
  ADD CONSTRAINT `ciudades_ibfk_1` FOREIGN KEY (`pais_id`) REFERENCES `paises` (`pais_id`);

--
-- Filtros para la tabla `comentarios`
--
ALTER TABLE `comentarios`
  ADD CONSTRAINT `comentarios_ibfk_1` FOREIGN KEY (`autor_id`) REFERENCES `users` (`id`);

--
-- Filtros para la tabla `dashboards`
--
ALTER TABLE `dashboards`
  ADD CONSTRAINT `dashboards_ibfk_1` FOREIGN KEY (`dashboard_id`) REFERENCES `dashboard_proyectos` (`dashboard_id`),
  ADD CONSTRAINT `dashboards_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

--
-- Filtros para la tabla `dashboard_proyectos`
--
ALTER TABLE `dashboard_proyectos`
  ADD CONSTRAINT `dashboard_proyectos_ibfk_1` FOREIGN KEY (`proj_id`) REFERENCES `proyectos` (`proj_id`);

--
-- Filtros para la tabla `miembros`
--
ALTER TABLE `miembros`
  ADD CONSTRAINT `miembros_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  ADD CONSTRAINT `miembros_ibfk_2` FOREIGN KEY (`rol_id`) REFERENCES `roles` (`rol_id`);

--
-- Filtros para la tabla `proyectos`
--
ALTER TABLE `proyectos`
  ADD CONSTRAINT `proyectos_ibfk_1` FOREIGN KEY (`team_id`) REFERENCES `team_members` (`team_id`);

--
-- Filtros para la tabla `tasksproject`
--
ALTER TABLE `tasksproject`
  ADD CONSTRAINT `tasksproject_ibfk_1` FOREIGN KEY (`estado_id`) REFERENCES `estados_task` (`estado_id`);

--
-- Filtros para la tabla `teams`
--
ALTER TABLE `teams`
  ADD CONSTRAINT `teams_ibfk_1` FOREIGN KEY (`team_id`) REFERENCES `team_members` (`team_id`),
  ADD CONSTRAINT `teams_ibfk_2` FOREIGN KEY (`proj_id`) REFERENCES `proyectos` (`proj_id`),
  ADD CONSTRAINT `teams_ibfk_3` FOREIGN KEY (`owner_id`) REFERENCES `users` (`id`);

--
-- Filtros para la tabla `team_members`
--
ALTER TABLE `team_members`
  ADD CONSTRAINT `team_members_ibfk_1` FOREIGN KEY (`member_id`) REFERENCES `miembros` (`user_id`);

--
-- Filtros para la tabla `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`pais_id`) REFERENCES `paises` (`pais_id`),
  ADD CONSTRAINT `users_ibfk_2` FOREIGN KEY (`ciudad_id`) REFERENCES `ciudades` (`city_id`),
  ADD CONSTRAINT `users_ibfk_3` FOREIGN KEY (`sexo_id`) REFERENCES `sexos` (`sx_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
