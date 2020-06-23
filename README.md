# Sample Integration between Azure AD B2C and IPification

The repository contains code which demonstrates an integration between [Azure AD B2C](https://docs.microsoft.com/en-us/azure/active-directory-b2c/overview) and [IPification](https://www.ipification.com/).

This integration is powered by the Identity Experience Framework in Azure AD B2C. For more information on TrustFramework Policies and the Identity Experience Framework, see the [Azure AD B2C documentation](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-overview).

[IPification](https://www.ipification.com/) is a highly secure, credential-less, network-based authentication solution for frictionless user experience on mobile and IoT devices.
For complete integration instructions to IPification solution please contact [IPification Team](https://www.ipification.com/contact-us). We will provide you neccessary account details and give you support in all integration phases.

## Demo site
In the `src/` folder is the source code for a simple website which uses OpenID Connect to authenticate using two Azure AD B2C policies which demonstrate different types of integration options with IPification.

You can find the demo site at: https://b2c-ipification-demo.azurewebsites.net

## Identity Experience Framework Policies

In the `policies/` folder you'll find custom policy definitions for two integration patterns with IPification:

### IPification as a passwordless authentication provider

The policies starting with `Phone` demonstrate how to leverage IPification as a silent phone-based passwordless authentication provider with fall back to SMS when IPification has not yet been supported with the certain mobile carrier.

This scenario is based on the [`phone-number-passwordless` starter pack](https://github.com/Azure-Samples/active-directory-b2c-custom-policy-starterpack/tree/master/scenarios/phone-number-passwordless).

### IPification as a second-factor authentication provider

The policies starting with `Mfa` demonstrate how to leverage IPification as a multi-factor authentication option to verify a user's mobile number silently. This will fall back to SMS when IPification has not yet been supported with the certain mobile carrier.

This scenario is based on the [`SocialAndLocalAccountsWithMfa` starter pack](https://github.com/Azure-Samples/active-directory-b2c-custom-policy-starterpack/tree/master/SocialAndLocalAccountsWithMfa).

## Deployment

This repository uses GitHub Actions to deploy both the Azure AD B2C policies and the website. You can find the deployment workflows in `.github/workflows/`.

## Community Help and Support
Use [Stack Overflow](https://stackoverflow.com/questions/tagged/azure-ad-b2c) to get support from the community. Ask your questions on Stack Overflow first and browse existing issues to see if someone has asked your question before. Make sure that your questions or comments are tagged with [azure-ad-b2c].

If you find a bug in the sample, please raise the issue on [GitHub Issues](https://github.com/azure-ad-b2c/deploy-trustframework-policy/issues).

To provide product feedback, visit the Azure AD B2C [feedback page](https://feedback.azure.com/forums/169401-azure-active-directory?category_id=160596).