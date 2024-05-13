package app;

import app.entity.City;
import app.entity.Ticket;
import app.entity.Train;
import app.entity.User;
import app.forms.FormAuthorization;
import app.forms.FormMain;
import app.service.TrainService;
import app.service.UserService;
import app.util.HebirnateUtil;
import jakarta.persistence.EntityManagerFactory;
import org.hibernate.cfg.Configuration;

import java.time.LocalDateTime;

public class App {
    private User user;

    App() {
        Configuration configuration = new Configuration();
        configuration.addAnnotatedClass(User.class);
        configuration.addAnnotatedClass(City.class);
        configuration.addAnnotatedClass(Train.class);
        configuration.addAnnotatedClass(Ticket.class);

        configuration.configure();

        try {
            EntityManagerFactory emf = configuration.buildSessionFactory();
            HebirnateUtil.emf = emf;
            showFormAuthorization();
        } catch (Exception e) {
            e.printStackTrace();
            HebirnateUtil.closeEntityManagerFactory();
        }
    }

    private void showFormAuthorization() {
        new FormAuthorization(this::onUserAuthenticated);
    }

    private void showFormMain(){
        new FormMain(user);
    }

    private void onUserAuthenticated(User user) {
        this.user = user;
        showFormMain();
    }

    public static void main(String[] args) {
        new App();
    }
}
