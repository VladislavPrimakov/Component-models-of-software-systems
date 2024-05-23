<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<body>
<c:if test="${not empty sessionScope.user}">
    <div>
        <span>
        <a href="/products">View product</a>
        </span>

        <c:if test="${sessionScope.user.role == 'MANAGER'}">
            <span>
                <a href="/add-product">Add product</a>
            </span>
        </c:if>

        <span>
        ${sessionScope.user.username} | ${sessionScope.user.role}
        </span>

        <span>
        <form action="/logout" method="post">
            <button type="submit">Logout</button>
        </form>
        </span>
    </div>
</c:if>
<c:if test="${empty sessionScope.user}">
<span>
        <a href="/login">Login</a>
    </span>
</c:if>
</body>
</html>
