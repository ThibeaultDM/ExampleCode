﻿using Orchestration.Interfaces;

namespace Orchestration.ObjectLayer;

public class QueasoBaseUrl : IQueasoBaseUrl
{
    private string url;
    private int CustomerPort;
    private string CustomerSubDomain;
    private int InvoicePort;
    private string InvoiceSubDomain;

    public QueasoBaseUrl(IConfiguration config)
    {
        url = config["ServiceUrl"] ?? throw new Exception("Could not fetch ServiceUrl");
        CustomerPort = config.GetValue<int>("CustomerPort");
        CustomerSubDomain = config["CustomerSubDomain"] ?? throw new Exception("Could not fetch CustomerSubDomain");
        InvoicePort = config.GetValue<int>("InvoicePort");
        InvoiceSubDomain = config["InvoiceSubDomain"] ?? throw new Exception("Could not fetch InvoiceSubDomain");
    }

    public string BaseUrlBuilder(BaseUrlComponent _baseUr1Component)
    {
        string baseUrl = url;
        switch (_baseUr1Component)
        {
            case BaseUrlComponent.customer:
            baseUrl += CustomerPort + CustomerSubDomain;
            break;

            case BaseUrlComponent.invoice:
            baseUrl += InvoicePort + InvoiceSubDomain;
            break;

            default: throw new ArgumentException("Invalid base URL component");
        }

        return baseUrl;
    }
}

public enum BaseUrlComponent
{
    customer,
    invoice
}