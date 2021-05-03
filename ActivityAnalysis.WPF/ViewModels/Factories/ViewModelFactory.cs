using System;
using ActivityAnalysis.WPF.State.Navigators;
using ActivityAnalysis.WPF.ViewModels.Authentication;

namespace ActivityAnalysis.WPF.ViewModels.Factories
{
    public class ViewModelFactory: IViewModelFactory
    {
        private readonly CreateViewModel<SignInViewModel> _createSignInViewModel;
        private readonly CreateViewModel<SignUpViewModel> _createSignUpViewModel;
        private readonly CreateViewModel<PasswordRecoveryViewModel> _createPasswordRecoveryViewModel;
        private readonly CreateViewModel<TimeAnalysisViewModel> _createTimeAnalysisViewModel;
        private readonly CreateViewModel<ProgramsAnalysisViewModel> _createProgramsAnalysisViewModel;

        public ViewModelFactory(CreateViewModel<SignInViewModel> createSignInViewModel,
            CreateViewModel<SignUpViewModel> createSignUpViewModel, 
            CreateViewModel<PasswordRecoveryViewModel> createPasswordRecoveryViewModel,
            CreateViewModel<TimeAnalysisViewModel> createTimeAnalysisViewModel,
            CreateViewModel<ProgramsAnalysisViewModel> createProgramsAnalysisViewModel)
        {
            _createSignInViewModel = createSignInViewModel;
            _createSignUpViewModel = createSignUpViewModel;
            _createPasswordRecoveryViewModel = createPasswordRecoveryViewModel;
            _createTimeAnalysisViewModel = createTimeAnalysisViewModel;
            _createProgramsAnalysisViewModel = createProgramsAnalysisViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.SignIn:
                    return _createSignInViewModel();
                case ViewType.SignUp:
                    return _createSignUpViewModel();
                case ViewType.PasswordRecovery:
                    return _createPasswordRecoveryViewModel();
                case ViewType.TimeAnalysis:
                    return _createTimeAnalysisViewModel();
                case ViewType.ProgramsAnalysis:
                    return _createProgramsAnalysisViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", viewType.ToString());
            }
        }
    }
}