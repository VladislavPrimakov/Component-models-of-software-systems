<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<html>
<head>
    <title>Products</title>
</head>
<body>
<%@include file="header.jsp" %>

<c:if test="${sessionScope.user.role == 'USER'}">
    <table border="1">
        <thead>
        <tr>
            <th>Product name</th>
            <th>Count</th>
            <th>Price</th>
            <th>Expiration date</th>
            <th>Placement</th>
        </tr>
        </thead>
        <tbody>
        <c:forEach items="${requestScope.products}" var="product">
            <tr>
                <th>${product.name}</th>
                <th>${product.count}</th>
                <th>${product.price}</th>
                <th>${product.expirationDate}</th>
                <th>${product.placement.name}</th>
            </tr>
        </c:forEach>
        </tbody>
    </table>
</c:if>

<c:if test="${sessionScope.user.role == 'MANAGER'}">
    <form action="/products" method="get">
        <label>Only in stock
            <c:choose>
                <c:when test="${param.inStock == 'true'}">
                    <input type="checkbox" name="inStock" value="true" checked>
                </c:when>
                <c:otherwise>
                    <input type="checkbox" name="inStock" value="true">
                </c:otherwise>
            </c:choose>
        </label>
        <br>
        <label>Expired
            <c:choose>
                <c:when test="${param.expired == 'true'}">
                    <input type="checkbox" name="expired" value="true" checked>
                </c:when>
                <c:otherwise>
                    <input type="checkbox" name="expired" value="true">
                </c:otherwise>
            </c:choose>
        </label>
        <br>
        <label>Placement:
            <select name="placementID">
                <c:choose>
                    <c:when test="${param.placementID == 0 or empty param.placementID}">
                        <option value="0" selected>All</option>
                    </c:when>
                    <c:otherwise>
                        <option value="0">All</option>
                    </c:otherwise>
                </c:choose>
                <c:forEach items="${requestScope.placements}" var="placement">
                    <c:choose>
                        <c:when test="${param.placementID == placement.id}">
                            <option value="${placement.id}" selected>${placement.name}</option>
                        </c:when>
                        <c:otherwise>
                            <option value="${placement.id}">${placement.name}</option>
                        </c:otherwise>
                    </c:choose>
                </c:forEach>
            </select>
        </label>
        <br>
        <button type="submit">Search</button>
    </form>
    <table border="1">
        <thead>
        <tr>
            <th>Product name</th>
            <th>Count</th>
            <th>Price</th>
            <th>Expiration date</th>
            <th>Placement</th>
            <th>Remove</th>
        </tr>
        </thead>
        <tbody>
        <c:forEach items="${requestScope.products}" var="product">
            <tr>
                <th>${product.name}</th>
                <th>
                    <form action="/update-product" method="post">
                        <input type="number" name="count" value="${product.count}">
                        <button type="submit" name="id" value="${product.id}">Update count</button>
                    </form>
                </th>
                <th>${product.price}</th>
                <th>${product.expirationDate}</th>
                <th>${product.placement.name}</th>
                <th>
                    <form action="/remove-product" method="post">
                        <button type="submit" name="id" value="${product.id}">Remove</button>
                    </form>
                </th>

            </tr>
        </c:forEach>
        </tbody>
    </table>
</c:if>

</body>
</html>
