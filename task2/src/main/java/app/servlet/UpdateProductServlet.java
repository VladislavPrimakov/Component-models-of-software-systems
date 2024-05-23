package app.servlet;


import app.service.ProductService;
import jakarta.ejb.EJB;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.util.Optional;

@WebServlet("/update-product")
public class UpdateProductServlet extends HttpServlet {
    @EJB
    private ProductService productService;

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        productService.updateProductById(
                Optional.ofNullable(req.getParameter("id")),
                Optional.ofNullable(req.getParameter("count"))
        );
        resp.sendRedirect(req.getHeader("referer"));
    }
}
