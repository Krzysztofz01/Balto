import { AuthService } from "src/app/authentication/services/auth.service";

export function authInitializer(authService: AuthService) {
    return () => new Promise(r => {
        authService.refresh().subscribe().add(r);
    });
}