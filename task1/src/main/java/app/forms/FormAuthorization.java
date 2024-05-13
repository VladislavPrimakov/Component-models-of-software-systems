package app.forms;

import app.entity.User;
import app.service.UserService;
import app.util.HebirnateUtil;
import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.Optional;
import java.util.function.Consumer;

public class FormAuthorization extends JFrame {
    private JPanel panelMain;
    private JLabel labelPassword;
    private JLabel labelLogin;
    private JTextField fieldLogin;
    private JPasswordField fieldPassword;
    private JButton logInButton;

    private final Consumer<User> onUserAuthenticated;

    public FormAuthorization(Consumer<User> onUserAuthenticated) {
        this.onUserAuthenticated = onUserAuthenticated;

        setTitle("Authorization");
        setContentPane(panelMain);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(800, 800);
        setVisible(true);
        logInButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                logIn();
            }
        });
    }

    public void logIn(){
        if (!fieldLogin.getText().isEmpty() && fieldPassword.getPassword().length > 0){
            UserService userService = new UserService(HebirnateUtil.getEntityManager());
            Optional<User> user = userService.auth(fieldLogin.getText(), String.valueOf(fieldPassword.getPassword()));
            if (user.isEmpty()){
                JOptionPane.showMessageDialog(this, "Invalid username or password");
            } else {
                onUserAuthenticated.accept(user.get());
                dispose();
            }
        }
    }
}

