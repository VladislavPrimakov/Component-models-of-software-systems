package app.filter;

import app.entity.Role;
import app.entity.User;
import jakarta.servlet.*;
import jakarta.servlet.annotation.WebFilter;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.util.Set;

@WebFilter("/*")
public class AuthorizationFilter implements Filter {

    private static final Set<String> PUBLIC_PATH = Set.of("/login", "/");
    private static final Set<String> MANAGER_PATH = Set.of("/remove-product", "/add-product", "/update-product");

    @Override
    public void doFilter(ServletRequest servletRequest, ServletResponse servletResponse, FilterChain filterChain) throws IOException, ServletException {
        var uri = ((HttpServletRequest) servletRequest).getRequestURI();
        if (isPublicPath(uri) || isUserLoggedIn(servletRequest, uri)) {
            filterChain.doFilter(servletRequest, servletResponse);
        } else {
            reject(servletRequest, servletResponse);
        }
    }

    private boolean isUserLoggedIn(ServletRequest servletRequest, String uri) {
        User user = (User) ((HttpServletRequest) servletRequest).getSession().getAttribute("user");
        if (user != null) {
            if (user.getRole() == Role.MANAGER && MANAGER_PATH.stream().anyMatch(uri::startsWith)) {
                return true;
            }
            if (uri.startsWith("/products") || uri.equals("/logout")) {
                return true;
            }
        } else {
            return false;
        }
        return false;
    }

    private boolean isPublicPath(String uri) {
        return PUBLIC_PATH.stream().anyMatch(uri::equals);
    }

    private void reject(ServletRequest servletRequest, ServletResponse servletResponse) throws IOException {
        ((HttpServletResponse) servletResponse).sendRedirect("/");
    }
}

