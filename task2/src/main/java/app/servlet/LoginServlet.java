package app.servlet;

import app.entity.User;
import app.service.Result;
import app.service.UserService;
import app.utils.JSPUtil;
import jakarta.ejb.EJB;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.util.Optional;

@WebServlet("/login")
public class LoginServlet extends HttpServlet {
    @EJB
    private UserService userService;

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        req.getRequestDispatcher(JSPUtil.getPath("login")).forward(req, resp);
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        Result<Optional<User>> result = userService.login(
                Optional.ofNullable(req.getParameter("username")),
                Optional.ofNullable(req.getParameter("password"))
        );
        if (result.hasErrors()) {
            req.setAttribute("username", req.getParameter("username"));
            req.setAttribute("errors", result.getErrors());
            doGet(req, resp);
        }
        if (result.getData().isPresent()) {
            req.getSession().setAttribute("user", result.getData().get());
            resp.sendRedirect("/");
        }
    }
}
