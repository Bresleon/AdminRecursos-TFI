# 🧾 Sistema de Gestión de Proveedores

Este proyecto tiene como objetivo desarrollar un **Sistema de Gestión de Proveedores** que permita registrar y administrar la información de **proveedores, técnicos, visitas y productos**, incluyendo sus calificaciones y los montos asociados a las visitas realizadas.  
El sistema busca optimizar la trazabilidad de los servicios prestados por los técnicos, el control de productos adquiridos o mantenidos, y la evaluación del desempeño de los proveedores.

---

## 🧑‍💻 Autores

- Breslauer, León - 56340
- Cavanna, Tiziana - 53163
- Díaz Alvillos, Bernabé - 56099
- Romano, Santiago - 53447
- Samaniego, Leopoldo Gabriel - 53602

Año: 2025

---

## 🧩 Módulos del Sistema

El sistema está dividido en cuatro módulos principales, alineados con los casos de uso representados en el diagrama:

### 1. **Módulo de Visitas**
Permite gestionar las visitas realizadas por los técnicos a los proveedores.

**Casos de uso:**
- Registrar visita  
- Modificar visita  

Cada visita puede tener un **concepto** (VENTA, REPARACIÓN o CONTROL) y un **estado** (por ejemplo, PENDIENTE, COMPLETADA, CANCELADA).  
El **monto total** de la visita se calcula automáticamente en base a los productos asociados en la tabla `productos_visitas`.

---

### 2. **Módulo de Técnicos**
Administra la información y desempeño de los técnicos asociados a los proveedores.

**Casos de uso:**
- Registrar técnico  
- Modificar técnico  
- Eliminar técnico  
- Calificar técnico  

Cada técnico trabaja para un único proveedor.  
La **calificación del técnico** se calcula automáticamente como el promedio de las calificaciones obtenidas en las visitas que realizó.

---

### 3. **Módulo de Proveedores**
Gestiona la información de los proveedores y su evaluación global.

**Casos de uso:**
- Registrar proveedor  
- Modificar proveedor  
- Eliminar proveedor  
- Calificar proveedor  
- Consultar monto total pagado  

La **calificación del proveedor** se obtiene automáticamente como el promedio de las calificaciones de sus técnicos asociados.

---

### 4. **Módulo de Productos**
Administra el catálogo de productos y su relación con las visitas realizadas.

**Casos de uso:**
- Registrar producto  
- Modificar producto  
- Eliminar producto  
- Consultar producto  

Cada registro en `productos_visitas` representa un producto específico (con número de serie y garantía) que fue **adquirido, reparado o controlado** durante una visita.

---

## 👨‍💼 Roles del Sistema

- Empleado: Registra y modifica visitas.
- Ejecutivo: Administra proveedores, técnicos y productos. Puede calificar y consultar informes.

---

## 🏗️ Arquitectura del Proyecto

El sistema sigue una arquitectura en **capas**, que facilita la separación de responsabilidades, el mantenimiento y la escalabilidad.

AdminRecursos-TFI/
│
├── README.md
├── docs/
│ ├── diagramas/
│ │ ├── casos_uso.png
│ │ ├── modelo_entidad_relacion.png
│ │ └── arquitectura.png
│ └── especificaciones.md
│
└── **src/**
  ├── **Dominio/ -> Entidades y Enums**
  ├── **Aplicacion/ -> Lógica de negocio, servicios y validaciones**
  ├── **Infraestructura/ -> Contexto de datos, repositorios y migraciones a base de datos**
  ├── **Presentacion/ -> Controladores, vistas y endpoints (UI)**
  └── **Tests/ -> Pruebas unitarias**

