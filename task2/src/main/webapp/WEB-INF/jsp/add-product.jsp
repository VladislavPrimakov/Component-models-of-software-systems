<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<html>
<head>
    <title>Add Product</title>
</head>
<body>
<%@ include file="header.jsp" %>
<form action="/add-product" method="post">
    <label>Name:
        <input type="text" name="name">
    </label>
    <br>
    <label>Count:
        <input type="number" name="count">
    </label>
    <br>
    <label>Price:
        <input type="number" name="price">
    </label>
    <br>
    <label>Expiration date:
        <input type="date" name="expiration">
    </label>
    <br>
    <br>
    <label>Placement:
        <select name="placementID">
            <c:forEach items="${requestScope.placements}" var="placement">
                <option value="${placement.id}">${placement.name}</option>
            </c:forEach>
        </select>
    </label>
    <br>
    <button type="submit">Send</button>
    <c:if test="${not empty requestScope.errors}">
        <div style="color: red">
            <c:forEach var="error" items="${requestScope.errors}">
                <span>${error}</span>
                <br>
            </c:forEach>
        </div>
    </c:if>
</form>
</body>
</html>
