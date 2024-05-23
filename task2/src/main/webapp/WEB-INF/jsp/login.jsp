<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<html>
<head>
    <title>Login</title>
</head>
<body>
<%@ include file="header.jsp" %>

<form action="/login" method="post">
    <label>Username:
        <input type="text" name="username" value="${requestScope.username}">
    </label>
    <br>
    <label>Password:
        <input type="password" name="password">
    </label>
    <br>
    <button type="submit">Login</button>
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
